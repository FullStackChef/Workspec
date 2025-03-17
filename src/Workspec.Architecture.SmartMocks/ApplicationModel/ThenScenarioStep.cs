namespace Workspec.Architecture.SmartMocks.ApplicationModel;

/// <summary>
/// Represents a "Then" step in a scenario with a generic result type.
/// </summary>
/// <typeparam name="TResult">The type of the expected result.</typeparam>
internal class ThenScenarioStep : ScenarioStep
{
    internal ThenScenarioStep(ScenarioStepType scenarioStepType, Func<IServiceProvider, string> descriptionFactory)
        : base(scenarioStepType, descriptionFactory)
    {
    }
}
