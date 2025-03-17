namespace Workspec.Architecture.SmartMocks.ApplicationModel;

/// <summary>
/// Represents a single step in a scenario with its type and a description factory.
/// </summary>
internal abstract class ScenarioStep
{
    public ScenarioStepType Type { get; }
    public Func<IServiceProvider, string> DescriptionFactory { get; }

    protected ScenarioStep(ScenarioStepType type, Func<IServiceProvider, string> descriptionFactory)
    {
        Type = type;
        DescriptionFactory = descriptionFactory;
    }

    /// <summary>
    /// Retrieves the description using the provided service provider.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve dependencies.</param>
    /// <returns>The generated description string.</returns>
    public string GetDescription(IServiceProvider serviceProvider) => DescriptionFactory(serviceProvider);
}
