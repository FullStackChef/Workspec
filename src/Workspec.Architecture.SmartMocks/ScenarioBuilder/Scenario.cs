using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Text;
using Workspec.Architecture.SmartMocks.ApplicationModel;

namespace Workspec.Architecture.SmartMocks.ScenarioBuilder
{
    public class Scenario
    {

        #region LoggerMessage.Define
        private static readonly Action<ILogger, string, Exception?> LogScenarioCreated =
            LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(1, nameof(LogScenarioCreated)),
                "Scenario '{ScenarioName}' has been created.");

        private static readonly Action<ILogger, ScenarioStepType, string, Exception?> LogStepAdded =
            LoggerMessage.Define<ScenarioStepType, string>(
                LogLevel.Information,
                new EventId(2, nameof(LogStepAdded)),
                "{StepType} step '{Description}' added.");
        #endregion

        private readonly ILogger _logger;
        private readonly List<ScenarioStep> _steps = [];
        private IServiceProvider _serviceProvider;

        public string ScenarioName { get; }

        public Scenario(IServiceProvider serviceProvider, string scenarioName)
        {
            _serviceProvider = serviceProvider;
            ScenarioName = scenarioName;
            _logger = serviceProvider.GetRequiredService<ILogger<Scenario>>();
            LogScenarioCreated(_logger, ScenarioName, null);
        }


        /// <summary>
        /// Adds a step to the scenario.
        /// </summary>
        /// <param name="scenarioStep">The scenario step to add.</param>
        /// <returns>The current <see cref="Scenario"/> instance.</returns>
        internal Scenario AddStep(ScenarioStep scenarioStep)
        {
            _steps.Add(scenarioStep);
            // Log the addition of the step with a placeholder description
            LogStepAdded(_logger, scenarioStep.Type, "Deferred Description", null);
            return this;
        }

        // Helper to generate the final scenario Gherkin output.
        public string ToGherkin()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Scenario: {ScenarioName}");
            foreach (var step in _steps)
            {
                // Indent each step to clearly associate them with the scenario.
                sb.AppendLine($"  {step.Type} {step.GetDescription(_serviceProvider)}");
            }

            return sb.ToString().TrimEnd();
        }


        internal IReadOnlyList<ScenarioStep> GetSteps() => _steps.AsReadOnly();

        internal Scenario WithServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            return this;
        }
    }
}
