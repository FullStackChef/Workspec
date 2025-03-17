// ThenScenarioBuilder.cs
using Moq;
using Workspec.Architecture.SmartMocks.ApplicationModel;

namespace Workspec.Architecture.SmartMocks.ScenarioBuilder
{
    public class ThenScenarioBuilder
    {
        private readonly Scenario _scenarioBuilder;

        internal ThenScenarioBuilder(Scenario scenarioBuilder)
        {
            _scenarioBuilder = scenarioBuilder;
        }

        public ThenScenarioBuilder And(Delegate validateAction)
        {
            Func<IServiceProvider, string> descriptionFactory = sp =>
                "And additional validations are executed.";

            var step = new ThenScenarioStep(ScenarioStepType.Then, descriptionFactory);
            _scenarioBuilder.AddStep(step);

            return this;
        }
    }
}
