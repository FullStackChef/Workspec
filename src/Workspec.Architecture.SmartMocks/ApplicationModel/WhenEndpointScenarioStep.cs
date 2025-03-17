namespace Workspec.Architecture.SmartMocks.ApplicationModel;

/// <summary>
/// Represents a "When" step in a scenario, configuring endpoints.
/// </summary>
internal class WhenEndpointScenarioStep(ScenarioStepType type, Func<IServiceProvider, string> descriptionFactory) : ScenarioStep(type, descriptionFactory);
