using Workspec.Architecture.SmartMocks.ApplicationModel;

namespace Workspec.Architecture.SmartMocks.ScenarioBuilder.Endpoints;

public class WhenEndpointScenario
{
    private readonly Scenario _scenarioBuilder;
    private readonly Endpoint _endpoint;

    internal WhenEndpointScenario(GivenScenario scenarioBuilder, Action<Endpoint> configureEndpoint)
    {
        _scenarioBuilder = scenarioBuilder;

        _endpoint = new Endpoint();
        configureEndpoint(_endpoint);
        _scenarioBuilder.AddStep(new WhenEndpointScenarioStep(ScenarioStepType.When, sp =>
            $"the {_endpoint.GetCurrentRoute()} endpoint is called using {_endpoint.GetCurrentMethod()}"));
    }

    public static implicit operator Scenario(WhenEndpointScenario whenEndpointScenario) => whenEndpointScenario._scenarioBuilder;
    public static implicit operator Endpoint(WhenEndpointScenario whenEndpointScenario) => whenEndpointScenario._endpoint;
}

public static class WhenEndpointScenarioExtensions
{
    /// <summary>
    /// Chains additional endpoint configuration.
    /// </summary>
    public static WhenEndpointScenario When(this GivenScenario givenScenario, Action<Endpoint> configureEndpoint)
    {

        return new WhenEndpointScenario(givenScenario, configureEndpoint);
    }
}