namespace Workspec.Architecture.SmartMocks.ApplicationModel;

internal record ScenarioStep(ScenarioStepType Type, string Description, Func<ValueTask> Action);
