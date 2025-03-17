using Microsoft.AspNetCore.Http;

namespace Workspec.Architecture.SmartMocks.ApplicationModel;

/// <summary>
/// Represents a "Given" step in a scenario, defining preconditions.
/// </summary>
internal class GivenScenarioStep : ScenarioStep
{
    private readonly Func<HttpContext, bool> _condition;

    internal GivenScenarioStep(Func<IServiceProvider, string> descriptionFactory, Func<HttpContext, bool> condition)
        : base(ScenarioStepType.Given, descriptionFactory)
    {
        _condition = condition;
    }

    /// <summary>
    /// Evaluates the "Given" condition.
    /// </summary>
    /// <param name="httpContext">The service provider.</param>
    /// <returns>True if the condition is met; otherwise, false.</returns>
    public bool Evaluate(HttpContext httpContext) => _condition(httpContext);
}
