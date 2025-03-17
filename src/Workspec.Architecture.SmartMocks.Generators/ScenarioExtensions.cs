using System;
using Workspec.Architecture.SmartMocks.Generators;
using Workspec.Architecture.SmartMocks.ScenarioBuilder;

#pragma warning disable IDE0130 // Namespace does not match folder structure - Reason: Discoverability
namespace Workspec.Architecture.SmartMocks;
#pragma warning restore IDE0130 // Namespace does not match folder structure - Reason: Discoverability

public static class ScenarioExtensions
{
    public static GivenScenario Given(this Scenario scenario, Func<GivenRepository, GivenScenario> selector)
    {
        var repository = new GivenRepository(scenario);
        return selector(repository);
    }
}
