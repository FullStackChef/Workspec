using Microsoft.Extensions.Logging;
using System.Text;
using Workspec.Architecture.SmartMocks.ApplicationModel;

namespace Workspec.Architecture.SmartMocks;

/// <summary>
/// Provides a fluent API for building and executing scenarios in the SmartMocks framework.
/// </summary>
public sealed class ScenarioBuilder
{
    private static readonly Func<ValueTask> s_DefaultFunc = static () => ValueTask.CompletedTask;



    /// <summary>
    /// Logs when a scenario is created.
    /// </summary>
    private static readonly Action<ILogger, string, Exception?> LogScenarioCreated =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, nameof(LogScenarioCreated)),
            "Scenario '{ScenarioName}' has been created.");

    /// <summary>
    /// Logs when a step is added to the scenario.
    /// </summary>
    private static readonly Action<ILogger, ScenarioStepType, string, Exception?> LogStepAdded =
        LoggerMessage.Define<ScenarioStepType, string>(
            LogLevel.Information,
            new EventId(2, nameof(LogStepAdded)),
            "{StepType} step '{Description}' added.");

    /// <summary>
    /// The name of the scenario being built.
    /// </summary>
    private readonly string _scenarioName;

    /// <summary>
    /// Logger instance used for logging scenario events.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// List of steps defined in the scenario.
    /// </summary>
    private readonly List<ScenarioStep> _steps = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="ScenarioBuilder"/> class.
    /// </summary>
    /// <param name="logger">The logger to use for logging events.</param>
    /// <param name="scenarioName">The name of the scenario being built.</param>
    internal ScenarioBuilder(ILogger logger, string scenarioName)
    {
        _logger = logger;
        _scenarioName = scenarioName;
        LogScenarioCreated(_logger, _scenarioName, null);
    }

    /// <summary>
    /// Starts the scenario with a "Given" step.
    /// </summary>
    /// <param name="description">The description of the "Given" step.</param>
    /// <param name="func">The action to perform for this step. Defaults to a no-op if not provided.</param>
    /// <returns>An instance of <see cref="GivenScenario"/> to continue building the scenario.</returns>
    public GivenScenario Given(string description, Func<ValueTask>? func = null) => new(this, description, func);

    /// <summary>
    /// Adds a step to the scenario with the specified type, description, and action.
    /// </summary>
    /// <param name="type">The type of the step (e.g., Given, When, Then).</param>
    /// <param name="description">The description of the step.</param>
    /// <param name="func">The action to perform for this step. Defaults to a no-op if not provided.</param>
    /// <returns>The current <see cref="ScenarioBuilder"/> instance.</returns>
    private ScenarioBuilder AddStep(ScenarioStepType type, string description, Func<ValueTask>? func = null)
    {
        func ??= s_DefaultFunc;
        _steps.Add(new ScenarioStep(type, description, func));
        LogStepAdded(_logger, type, description, null);
        return this;
    }

    /// <summary>
    /// Outputs the scenario steps as Gherkin syntax.
    /// </summary>
    /// <returns>A string representing the scenario in Gherkin syntax.</returns>
    public string ToGherkin()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Scenario: {_scenarioName}");

        foreach (var step in _steps)
        {
            sb.AppendLine(MapStepToGherkin(step));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Maps the <see cref="ScenarioStep"/> to a Gherkin syntax string.
    /// </summary>
    /// <param name="step">The step to map.</param>
    /// <returns>The Gherkin syntax corresponding to the step.</returns>
    private static string MapStepToGherkin(ScenarioStep step) => step.Type switch
    {
        ScenarioStepType.Given => $"Given {step.Description}",
        ScenarioStepType.AndGiven => $"And {step.Description}",
        ScenarioStepType.When => $"When {step.Description}",
        ScenarioStepType.AndWhen => $"And {step.Description}",
        ScenarioStepType.Then => $"Then {step.Description}",
        ScenarioStepType.AndThen => $"And {step.Description}",
        _ => throw new ArgumentOutOfRangeException(nameof(step.Type), step.Type, "Invalid step type.")
    };

    /// <summary>
    /// Builder for "Given" steps.
    /// </summary>
    public sealed class GivenScenario
    {
        private readonly ScenarioBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="GivenScenario"/> class.
        /// </summary>
        /// <param name="builder">The parent <see cref="ScenarioBuilder"/>.</param>
        /// <param name="description">The description of the "Given" step.</param>
        /// <param name="func">The action to perform for this step. Defaults to a no-op if not provided.</param>
        internal GivenScenario(ScenarioBuilder builder, string description, Func<ValueTask>? func = null)
        {
            _builder = builder;
            _builder.AddStep(ScenarioStepType.Given, description, func);
        }

        /// <summary>
        /// Adds an "AndGiven" step to the scenario.
        /// </summary>
        /// <param name="description">The description of the step.</param>
        /// <param name="func">The action to perform for this step. Defaults to a no-op if not provided.</param>
        /// <returns>The current <see cref="GivenScenario"/> instance.</returns>
        public GivenScenario And(string description, Func<ValueTask>? func = null)
        {
            _builder.AddStep(ScenarioStepType.AndGiven, description, func);
            return this;
        }

        /// <summary>
        /// Transitions to a "When" step.
        /// </summary>
        /// <param name="description">The description of the "When" step.</param>
        /// <param name="func">The action to perform for this step. Defaults to a no-op if not provided.</param>
        /// <returns>An instance of <see cref="WhenScenario"/> to continue building the scenario.</returns>
        public WhenScenario When(string description, Func<ValueTask>? func = null) => new(_builder, description, func);
    }

    /// <summary>
    /// Builder for "When" steps.
    /// </summary>
    public sealed class WhenScenario
    {
        private readonly ScenarioBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhenScenario"/> class.
        /// </summary>
        /// <param name="builder">The parent <see cref="ScenarioBuilder"/>.</param>
        /// <param name="description">The description of the "When" step.</param>
        /// <param name="func">The action to perform for this step. Defaults to a no-op if not provided.</param>
        internal WhenScenario(ScenarioBuilder builder, string description, Func<ValueTask>? func = null)
        {
            _builder = builder;
            _builder.AddStep(ScenarioStepType.When, description, func);
        }

        /// <summary>
        /// Adds an "AndWhen" step to the scenario.
        /// </summary>
        /// <param name="description">The description of the step.</param>
        /// <param name="func">The action to perform for this step. Defaults to a no-op if not provided.</param>
        /// <returns>The current <see cref="WhenScenario"/> instance.</returns>
        public WhenScenario And(string description, Func<ValueTask>? func = null)
        {
            _builder.AddStep(ScenarioStepType.AndWhen, description, func);
            return this;
        }

        /// <summary>
        /// Transitions to a "Then" step.
        /// </summary>
        /// <param name="description">The description of the "Then" step.</param>
        /// <param name="func">The action to perform for this step. Defaults to a no-op if not provided.</param>
        /// <returns>An instance of <see cref="ThenScenario"/> to continue building the scenario.</returns>
        public ThenScenario Then(string description, Func<ValueTask>? func = null) => new(_builder, description, func);
    }

    /// <summary>
    /// Builder for "Then" steps.
    /// </summary>
    public sealed class ThenScenario
    {
        private readonly ScenarioBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenScenario"/> class.
        /// </summary>
        /// <param name="builder">The parent <see cref="ScenarioBuilder"/>.</param>
        /// <param name="description">The description of the "Then" step.</param>
        /// <param name="func">The action to perform for this step. Defaults to a no-op if not provided.</param>
        internal ThenScenario(ScenarioBuilder builder, string description, Func<ValueTask>? func = null)
        {
            _builder = builder.AddStep(ScenarioStepType.Then, description, func);
        }

        /// <summary>
        /// Adds an "AndThen" step to the scenario.
        /// </summary>
        /// <param name="description">The description of the step.</param>
        /// <param name="func">The action to perform for this step. Defaults to a no-op if not provided.</param>
        /// <returns>The current <see cref="ThenScenario"/> instance.</returns>
        public ThenScenario And(string description, Func<ValueTask>? func = null)
        {
            _builder.AddStep(ScenarioStepType.AndThen, description, func);
            return this;
        }

        /// <summary>
        /// Implicitly converts a <see cref="ThenScenario"/> back to its parent <see cref="ScenarioBuilder"/>.
        /// </summary>
        /// <param name="thenScenario">The "Then" scenario to convert.</param>
        public static implicit operator ScenarioBuilder(ThenScenario thenScenario) => thenScenario._builder;
    }

    /// <summary>
    /// Gets a read-only list of the steps defined in the scenario.
    /// </summary>
    /// <remarks>
    /// This property is intended for internal use and is primarily exposed for testing purposes. 
    /// It allows inspection of the steps added to the scenario during unit tests.
    /// </remarks>
    internal IReadOnlyList<ScenarioStep> Steps => _steps.AsReadOnly();
}
