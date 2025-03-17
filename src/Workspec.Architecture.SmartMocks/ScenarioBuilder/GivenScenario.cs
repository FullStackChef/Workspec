// GivenScenarioBuilder.cs
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using Workspec.Architecture.SmartMocks.ApplicationModel;

namespace Workspec.Architecture.SmartMocks.ScenarioBuilder;

public class GivenScenario
{
    private readonly Scenario _scenarioBuilder;

    internal GivenScenario(Scenario scenarioBuilder)
    {
        _scenarioBuilder = scenarioBuilder;
    }

    public GivenScenario And<T, TProp>(Expression<Func<T, TProp>> memberExpression, TProp expectedValue) where T : class
    {
        var step = new GivenScenarioStep(
            sp =>
            {
                var mock = sp.GetService<SmartMock<T>>()
                    ?? throw new InvalidOperationException($"SmartMock<{typeof(T).Name}> is not registered.");
                var description = mock.DescriptionFor(memberExpression, expectedValue);
                return $"And {description}";
            },
            ctx =>
            {
                var compiled = memberExpression.Compile();
                var mock = ctx.RequestServices.GetService<SmartMock<T>>()
                    ?? throw new InvalidOperationException($"SmartMock<{typeof(T).Name}> is not registered.");
                var actual = compiled.Invoke(mock.Object);
                return actual != null && actual.Equals(expectedValue);
            }
        );
        _scenarioBuilder.AddStep(step);
        return this;
    }

    public static implicit operator Scenario(GivenScenario givenScenario) => givenScenario._scenarioBuilder;
}
public static class GivenScenarioExtensions
{
    /// <summary>
    /// Extension method for starting a "Given" scenario.
    /// </summary>
    /// <typeparam name="T">The service type.</typeparam>
    /// <typeparam name="TProp">The property type.</typeparam>
    /// <param name="scenario">The scenario entry point.</param>
    /// <param name="memberExpression">Expression to access the property.</param>
    /// <param name="expectedValue">The expected value for the property.</param>
    /// <returns>A new <see cref="GivenScenario"/> builder.</returns>
    public static GivenScenario Given<T, TProp>(
        this Scenario scenario,
        Expression<Func<T, TProp>> memberExpression,
        TProp expectedValue)
        where T : class
    {
        // Build the description and condition factories.
        Func<IServiceProvider, string> descriptionFactory = sp =>
        {
            var mock = sp.GetService<SmartMock<T>>() ??
                throw new InvalidOperationException($"SmartMock<{typeof(T).Name}> is not registered.");
            return mock.DescriptionFor(memberExpression, expectedValue);
        };

        Func<HttpContext, bool> conditionFactory = ctx =>
        {
            var compiled = memberExpression.Compile();
            var mock = ctx.RequestServices.GetService<SmartMock<T>>() ??
                throw new InvalidOperationException($"SmartMock<{typeof(T).Name}> is not registered.");
            var actualValue = compiled(mock.Object);
            return actualValue != null && actualValue.Equals(expectedValue);
        };

        // Create the Given step and add it to the scenario.
        var step = new GivenScenarioStep(descriptionFactory, conditionFactory);
        scenario.AddStep(step);

        // Return a new GivenScenario builder that wraps the scenario.
        return new GivenScenario(scenario);
    }

    public static GivenScenario Given<T>(this Scenario scenario, Expression<Func<T, bool>> memberExpression, bool expectedValue)
        where T : class => scenario.Given<T, bool>(memberExpression, expectedValue);
}
