# ScenarioBuilder

## Overview
The `ScenarioBuilder` class provides a fluent API for building scenarios using Gherkin syntax. It allows developers to define `Given`, `When`, and `Then` steps for testing and documentation purposes. The `ScenarioBuilder` ensures proper chaining of steps and logs each action for better traceability.

---

## Usage

### Initialization
To create a new scenario:

```csharp
var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger<ScenarioBuilder>();

var builder = new ScenarioBuilder(logger, "User Login Scenario");
```

### Defining Steps
Use the fluent API to define your scenario steps:

```csharp
builder
    .Given("a user is registered")
    .And("the user is on the login page")
    .When("the user enters valid credentials")
    .And("clicks the login button")
    .Then("the user is redirected to the dashboard")
    .And("a welcome message is displayed");
```

### Outputting Gherkin Syntax
To generate the Gherkin syntax for your scenario:

```csharp
Console.WriteLine(builder.ToGherkin());
```

#### Example Output:
```gherkin
Scenario: User Login Scenario
Given a user is registered
And the user is on the login page
When the user enters valid credentials
And clicks the login button
Then the user is redirected to the dashboard
And a welcome message is displayed
```

---

## Logging
`ScenarioBuilder` logs the following events:

1. **Scenario Creation**:
   - Example: `Scenario 'User Login Scenario' has been created.`
2. **Step Addition**:
   - Example: `Given step 'a user is registered' added.`

---

## Unit Testing
To validate your scenarios, use the `ToGherkin` method in your unit tests. For example:

### Example Test:
```csharp
[Fact]
public void ScenarioBuilder_ShouldOutputCorrectGherkinSyntax()
{
    // Arrange
    var logger = Mock.Of<ILogger>();
    var builder = new ScenarioBuilder(logger, "Test Scenario");

    builder
        .Given("a user is logged in")
        .When("the user requests data")
        .Then("the data is returned");

    // Act
    var gherkin = builder.ToGherkin();

    // Assert
    var expected = """Scenario: Test Scenario
Given a user is logged in
When the user requests data
Then the data is returned""";
    Assert.Equal(expected, gherkin.Trim());
}
```

---

## API Reference

### `ScenarioBuilder`
- **Constructor**: `ScenarioBuilder(ILogger logger, string scenarioName)`
    - `logger`: Logger instance for logging events.
    - `scenarioName`: Name of the scenario.

- **Methods**:
  - `Given(string description, Func<ValueTask>? func = null)`: Starts the scenario with a `Given` step.
  - `ToGherkin()`: Outputs the scenario in Gherkin syntax.

### `GivenScenario`
- **Methods**:
  - `And(string description, Func<ValueTask>? func = null)`: Adds an additional `Given` step.
  - `When(string description, Func<ValueTask>? func = null)`: Transitions to a `When` step.

### `WhenScenario`
- **Methods**:
  - `And(string description, Func<ValueTask>? func = null)`: Adds an additional `When` step.
  - `Then(string description, Func<ValueTask>? func = null)`: Transitions to a `Then` step.

### `ThenScenario`
- **Methods**:
  - `And(string description, Func<ValueTask>? func = null)`: Adds an additional `Then` step.

---

## Best Practices
- Use the `ToGherkin` method to document your test cases in Gherkin syntax.
- Leverage logging for debugging and monitoring scenario execution.

---

