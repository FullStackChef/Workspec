namespace Workspec.Architecture.SmartMocks.ApplicationModel;

internal class WhenScenarioStep(ScenarioStepType type, Func<IServiceProvider, string> descriptionFactory) : ScenarioStep(type, descriptionFactory);