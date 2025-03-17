# ScenarioBuilder

## Overview
The `ScenarioBuilder` class provides a fluent API for building scenarios using Gherkin syntax. It allows developers to define `Given`, `When`, and `Then` steps for testing and documentation purposes. The `ScenarioBuilder` ensures proper chaining of steps, supports structured logging, and integrates seamlessly into unit testing workflows.

---

## Features

1. **Fluent API**:
   - Easily chain `Given`, `When`, and `Then` steps to define scenarios.

2. **Gherkin Syntax Generation**:
   - Automatically generates human-readable Gherkin syntax from defined steps.

3. **Structured Logging**:
   - Logs scenario creation, step addition, and other actions for better traceability.

4. **Unit Test Integration**:
   - Simplifies scenario validation by providing a `ToGherkin` method for testing expected outputs.

5. **Support for Mock Integration**:
   - Combine with tools like `MockBuilder` for mocking and validating dependencies.

---

## Getting Started

### Initialization

To create a new scenario, instantiate the `ScenarioBuilder` with a logger and a scenario name:

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

`ScenarioBuilder` supports structured logging to provide insights into scenario execution. These are the key log events:

- **Scenario Created**: Logged when a new scenario is initialized.
- **Step Added**: Logged when a step is added to the scenario.

### Example Log Messages:

- *Information*: "Scenario 'User Login Scenario' has been created."
- *Information*: "Given step 'a user is registered' added."
- *Information*: "Then step 'the user is redirected to the dashboard' added."

---

## Advanced Usage

### Integrating with MockBuilder

`ScenarioBuilder` can be combined with `MockBuilder` for scenarios requiring mock setup and validation. For example:

```csharp
builder
    .Given("a user service is mocked")
    .WithMockSetup<IUserService>(mock => mock.Setup(s => s.GetUser()).Returns(new User { Name = "Test User" }))
    .When("the user data is requested")
    .Then("the mock should be validated")
    .ValidateMock<IUserService>(mock => mock.Verify(s => s.GetUser(), Times.Once()));
```

### Asynchronous Steps

You can define asynchronous actions for each step using the optional `Func<ValueTask>` parameter:

```csharp
builder
    .Given("a user is logged in", async () => await Task.Delay(100))
    .When("the user requests data", async () => await FetchData())
    .Then("the data is displayed", async () => await DisplayData());
```

---

## Unit Testing

To validate scenarios, use the `ToGherkin` method in your unit tests.

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

### Nested Classes:

#### `GivenScenario`
- **Methods**:
  - `And(string description, Func<ValueTask>? func = null)`: Adds an additional `Given` step.
  - `When(string description, Func<ValueTask>? func = null)`: Transitions to a `When` step.

#### `WhenScenario`
- **Methods**:
  - `And(string description, Func<ValueTask>? func = null)`: Adds an additional `When` step.
  - `Then(string description, Func<ValueTask>? func = null)`: Transitions to a `Then` step.

#### `ThenScenario`
- **Methods**:
  - `And(string description, Func<ValueTask>? func = null)`: Adds an additional `Then` step.

---

## Best Practices

1. **Document Scenarios**:
   - Use the `ToGherkin` method to document your test cases in Gherkin syntax for easy sharing and readability.

2. **Use Logging**:
   - Enable logging to monitor scenario execution and troubleshoot issues.

3. **Integrate with Mocks**:
   - Combine with `MockBuilder` to test interactions with dependencies.

4. **Reuse Steps**:
   - Group common steps into helper methods or classes to avoid duplication.

---

## FAQs

### Can I use custom step types?
Currently, the builder supports `Given`, `When`, and `Then` steps, along with their chained counterparts like `And`. For custom step types, extend the builder as needed.

### Does `ScenarioBuilder` support parallel execution?
Each scenario runs sequentially as defined. For parallelism, define independent scenarios and execute them concurrently.

### How can I customize logging?
Provide a custom implementation of `ILogger` or configure your logging framework to handle logs as needed.

---

## Conclusion

`ScenarioBuilder` simplifies the process of defining, documenting, and validating scenarios in your tests. By leveraging its fluent API, Gherkin syntax generation, and structured logging, you can create clear and maintainable test cases that improve collaboration and understanding across your team.

For more advanced scenarios, explore its integration with `MockBuilder` or extend its functionality to meet your needs.

