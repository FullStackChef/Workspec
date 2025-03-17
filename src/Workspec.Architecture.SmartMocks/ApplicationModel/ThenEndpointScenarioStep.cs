using Microsoft.AspNetCore.Http;
using Workspec.Architecture.SmartMocks.ScenarioBuilder;

namespace Workspec.Architecture.SmartMocks.ApplicationModel;

/// <summary>
/// Represents a "Then" step in a scenario, validating endpoint results.
/// </summary>
internal class ThenEndpointScenarioStep : ScenarioStep
{
    private readonly Endpoint _configuredEndpoint;
    private readonly Delegate _handler;
    internal ThenEndpointScenarioStep(ScenarioStepType type, Endpoint configuredEndpoint, Delegate handler, Func<IServiceProvider, string> descriptionFactory) : base(type, descriptionFactory)
    {
        _configuredEndpoint = configuredEndpoint;
        _handler = handler;
    }

    public static implicit operator Endpoint(ThenEndpointScenarioStep step) => step._configuredEndpoint;
    public ValueTask<IResult> Handle(HttpContext context, Scenario scenario) => (ValueTask<IResult>)_handler.DynamicInvoke(context, scenario);
}
