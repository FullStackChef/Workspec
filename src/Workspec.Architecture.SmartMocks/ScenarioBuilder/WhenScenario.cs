using Workspec.Architecture.SmartMocks.ApplicationModel;

namespace Workspec.Architecture.SmartMocks.ScenarioBuilder;

public class WhenScenario
{
    private readonly Scenario _scenarioBuilder;

    internal WhenScenario(Scenario scenarioBuilder)
    {
        _scenarioBuilder = scenarioBuilder;
    }

    /// <summary>
    /// Chains an additional standard action.
    /// </summary>
    public WhenScenario And(Action configurationAction)
    {

        // Add a step with a generic description.
        Func<IServiceProvider, string> descriptionFactory = sp =>
            "A standard when action is executed.";

        var step = new WhenScenarioStep(ScenarioStepType.When, descriptionFactory);
        _scenarioBuilder.AddStep(step);

        return this;
    }
}
