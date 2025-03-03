using Microsoft.AspNetCore.Http;
using Workspec.Architecture.SmartMocks.ApplicationModel;

namespace Workspec.Architecture.SmartMocks.ScenarioBuilder.Endpoints;

public class ThenEndpointScenario
{
    private Scenario _scenarioBuilder;
    internal ThenEndpointScenario(WhenEndpointScenario scenario, Func<IServiceProvider, string> descriptionFactory, Delegate handler)
    {
        _scenarioBuilder = scenario;
        _scenarioBuilder.AddStep(new ThenEndpointScenarioStep(ScenarioStepType.Then, scenario,
            handler, descriptionFactory));
    }

    public static implicit operator Scenario(ThenEndpointScenario thenEndpointScenario) => thenEndpointScenario._scenarioBuilder;

}
public static class ThenEndpointScenarioExtensions
{
    /// <summary>
    /// Chains additional endpoint configuration.
    /// </summary>
    public static ThenEndpointScenario Then(this WhenEndpointScenario whenEndpointScenario, Func<IServiceProvider, string> descriptionFactory, Func<HttpContext, Scenario, ValueTask<IResult>> handler)
    {
        return new ThenEndpointScenario(whenEndpointScenario, descriptionFactory, handler);
    }
}