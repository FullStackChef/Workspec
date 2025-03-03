using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Workspec.Architecture.SmartMocks.ApplicationModel;
using Workspec.Architecture.SmartMocks.ScenarioBuilder;

namespace Workspec.Architecture.SmartMocks;
public static class ApplicationBuilderExtensions
{
    public static FeatureBuilder ForFeature(this WebApplication application,
                                        string featureName,
                                        string routePrefix,
                                        string? description = null) => new(application, featureName, routePrefix, description);
}
public class FeatureBuilder
{
    internal readonly string _featureName;
    internal readonly string? _description;

    internal readonly RouteGroupBuilder _featureGroup;
    internal readonly IServiceProvider _serviceProvider;
    internal readonly FeatureRegistry _featureRegistry;
    private readonly ILogger<FeatureBuilder> _logger;

    internal FeatureBuilder(WebApplication webApplication, string featureName, string routePrefix, string? description = null)
    {
        _featureName = featureName;
        _description = description;
        _serviceProvider = webApplication.Services;
        _logger = _serviceProvider.GetRequiredService<ILogger<FeatureBuilder>>();
        _featureRegistry = _serviceProvider.GetRequiredService<FeatureRegistry>();
        _featureGroup = webApplication.MapGroup(routePrefix).WithDisplayName(_featureName);
    }

    /// <summary>
    /// Defines a version for the feature.
    /// </summary>
    /// <param name="version">The version string (e.g., "v1").</param>
    /// <returns>A <see cref="FeatureVersionBuilder"/> to define scenarios.</returns>
    public FeatureVersionBuilder WithVersion(string version)
    {
        var versionBuilder = new FeatureVersionBuilder(this, _featureGroup, version);
        _featureRegistry.RegisterFeature(versionBuilder);
        return versionBuilder;
    }

    /// <summary>
    /// Builder for defining scenarios within a feature version.
    /// </summary>
    public class FeatureVersionBuilder
    {
        internal readonly FeatureBuilder _builder;
        private readonly RouteGroupBuilder _versionGroup;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FeatureVersionBuilder> _logger;
        private readonly List<Scenario> _scenarios = [];

        public string Version { get; }

        internal FeatureVersionBuilder(FeatureBuilder featureBuilder, RouteGroupBuilder featureGroup, string version)
        {
            _builder = featureBuilder;
            Version = version;
            _versionGroup = featureGroup.MapGroup($"/{version}");
            _serviceProvider = featureBuilder._serviceProvider;
            _logger = _serviceProvider.GetRequiredService<ILogger<FeatureVersionBuilder>>();
        }

        /// <summary>
        /// Defines scenarios for the feature version.
        /// </summary>
        /// <param name="featureScenarioConfiguration">Configuration action for scenarios.</param>
        /// <returns>The parent <see cref="FeatureBuilder"/>.</returns>
        public FeatureBuilder WithScenarios(Action<FeatureScenarioCollectionBuilder> featureScenarioConfiguration)
        {
            var scenarioCollectionBuilder = new FeatureScenarioCollectionBuilder(_serviceProvider, _scenarios);
            featureScenarioConfiguration(scenarioCollectionBuilder);

            var endpointScenarios = _scenarios.Where(s => s.GetSteps().OfType<ThenEndpointScenarioStep>().FirstOrDefault() is ThenEndpointScenarioStep).ToList();

            var groupedScenarios = endpointScenarios.GroupBy(s => (Endpoint)s.GetSteps().OfType<ThenEndpointScenarioStep>().First()).ToList();

            foreach (var group in groupedScenarios)
            {
                _versionGroup.MapEndpointStep(group.Key, async (HttpContext context) =>
                    {
                        foreach (var scenario in group)
                        {
                            var given = scenario.GetSteps().OfType<GivenScenarioStep>();
                            if (given.All(g => g.Evaluate(context)))
                            {
                                return await scenario.GetSteps().OfType<ThenEndpointScenarioStep>().First().Handle(context, scenario.WithServiceProvider(context.RequestServices));
                            }
                        }
                        return Results.NotFound(new { Message = "Not Found" });
                    });
            }

            return _builder;
        }

        /// <summary>
        /// Retrieves all scenarios defined for this feature version.
        /// </summary>
        public IReadOnlyList<Scenario> GetScenarios() => _scenarios.AsReadOnly();
    }

    /// <summary>
    /// Builder for collecting scenarios within a feature version.
    /// </summary>
    public class FeatureScenarioCollectionBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FeatureScenarioCollectionBuilder> _logger;
        private readonly List<Scenario> _scenarios;

        public FeatureScenarioCollectionBuilder(IServiceProvider serviceProvider, List<Scenario> scenarios)
        {
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<ILogger<FeatureScenarioCollectionBuilder>>();
            _scenarios = scenarios;
        }

        /// <summary>
        /// Adds a scenario to the collection.
        /// </summary>
        /// <param name="name">The name of the scenario.</param>
        /// <param name="scenarioConfigurationAction">Configuration action for the scenario.</param>
        /// <returns>The current <see cref="FeatureScenarioCollectionBuilder"/> instance.</returns>
        public FeatureScenarioCollectionBuilder WithScenario(string name, Action<Scenario> scenarioConfigurationAction)
        {
            var scenario = new Scenario(_serviceProvider, name);
            scenarioConfigurationAction(scenario);
            _scenarios.Add(scenario);
            return this;
        }
    }
}