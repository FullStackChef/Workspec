using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Workspec.Architecture.SmartMocks.ApiBuilder;

namespace Workspec.Architecture.SmartMocks;
public static class ApiRouteBuilderExtensions
{
    public static ApiBuilder ForFeature(this WebApplication application,
                                        string featureName,
                                        string routePrefix) => new(application, featureName, routePrefix);
}
public class ApiBuilder
{
    private readonly RouteGroupBuilder _featureGroup;
    private readonly ILogger<ApiBuilder> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _featureName;

    internal ApiBuilder(WebApplication webApplication, string featureName, string routePrefix)
    {
        _featureName = featureName;
        _serviceProvider = webApplication.Services;
        _featureGroup = webApplication.MapGroup(routePrefix).WithDisplayName(_featureName);
        _logger = _serviceProvider.GetRequiredService<ILogger<ApiBuilder>>();
    }

    public ApiVersionBuilder WithVersion(string version) => new(_serviceProvider, _featureGroup, version);

    public class ApiVersionBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RouteGroupBuilder _versionGroup;
        private readonly ILogger<ApiVersionBuilder> _logger;

        internal ApiVersionBuilder(IServiceProvider serviceProvider, RouteGroupBuilder featureGroup, string version)
        {
            _serviceProvider = serviceProvider;
            _versionGroup = featureGroup.MapGroup(version);
            _logger = serviceProvider.GetRequiredService<ILogger<ApiVersionBuilder>>();
        }

        public ApiVersionBuilder WithGetEndpoint(string route, Action<EndpointBuilder> action)
        {
            var endpoint = new EndpointBuilder(_serviceProvider, HttpMethod.Get, route);
            action(endpoint);
            _versionGroup.MapGet(((Endpoint)endpoint).Route, async (HttpContext context) =>
            {
                return((Endpoint)endpoint).Scenarios.First().ToGherkin();
            });
            return this;
        }
        public ApiVersionBuilder WithPostEndpoint(string route, Action<EndpointBuilder> action)
        {
            var endpoint = new EndpointBuilder(_serviceProvider, HttpMethod.Post, route);
            action(endpoint);
            return this;
        }
        public ApiVersionBuilder WithPutEndpoint(string route, Action<EndpointBuilder> action)
        {
            var endpoint = new EndpointBuilder(_serviceProvider, HttpMethod.Put, route);
            action(endpoint);
            return this;
        }
        public ApiVersionBuilder WithDeleteEndpoint(string route, Action<EndpointBuilder> action)
        {
            var endpoint = new EndpointBuilder(_serviceProvider, HttpMethod.Delete, route);
            action(endpoint);
            return this;
        }
        public ApiVersionBuilder WithPatchEndpoint(string route, Action<EndpointBuilder> action)
        {
            var endpoint = new EndpointBuilder(_serviceProvider, HttpMethod.Patch, route);
            action(endpoint);
            return this;
        }
    }
    public class EndpointBuilder(IServiceProvider services, HttpMethod method, string route)
    {
        private readonly HttpMethod _method = method;
        private readonly string _route = route;
        private readonly List<Scenario> _scenarios = [];

        public EndpointBuilder WithScenario(string scenario, Action<Scenario> ConfigureScenario)
        {
            var logger = services.GetRequiredService<ILogger<Scenario>>();
            Scenario senario = new(logger, scenario);
            ConfigureScenario(senario);
            _scenarios.Add(senario);
            return this;
        }

        public static implicit operator Endpoint(EndpointBuilder builder) =>
            new(builder._method, builder._route, builder._scenarios);
    }

    public record Endpoint(HttpMethod Method, string Route, List<Scenario> Scenarios);
}
