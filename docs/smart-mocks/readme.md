# SmartMocks

## Overview
SmartMocks is a .NET framework designed to streamline the process of building, testing, and documenting APIs and workflows. It combines the power of fluent APIs, Gherkin syntax, and modern mocking techniques to simplify early integration testing and ensure accurate documentation.

---

## Key Features

1. **Scenario Builder**:
   - Define test scenarios in a Gherkin-like syntax using a fluent API.
   - Output scenarios in Gherkin format for documentation and BDD purposes.

2. **Mocking Integration**:
   - Leverage frameworks like Moq to integrate SmartMocks into your test suite.
   - Register and validate mock setups effortlessly.

3. **Workflow Engine (Planned Feature)**:
   - Execute defined steps to simulate workflows and validate behaviors programmatically.

4. **Structured Logging**:
   - Track scenario creation, step additions, and validations using `Microsoft.Extensions.Logging`.

5. **Modular and Extensible**:
   - Built for customization and adaptability to various testing and documentation needs.

---

## Installation

### NuGet Package
To install SmartMocks, add the following NuGet package to your project:

```bash
Install-Package Workspec.Architecture.SmartMocks
```

### .NET CLI

```bash
dotnet add package Workspec.Architecture.SmartMocks
```

### Dependencies
Make sure to include the required dependencies, such as:

- `Microsoft.Extensions.Logging`
- `Moq` (for mocking support)

---

## Components

### 1. ScenarioBuilder
The `ScenarioBuilder` class provides a fluent API for defining scenarios in Gherkin syntax. It ensures proper chaining of steps (`Given`, `When`, `Then`) and supports logging for each action.

#### Example Usage

```csharp
var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger<ScenarioBuilder>();

var builder = new ScenarioBuilder(logger, "User Login Scenario")
    .Given("a user is registered")
    .When("the user logs in")
    .Then("the dashboard is displayed");

Console.WriteLine(builder.ToGherkin());
```

#### Example Output

```gherkin
Scenario: User Login Scenario
Given a user is registered
When the user logs in
Then the dashboard is displayed
```

Refer to the [ScenarioBuilder Documentation](scenario_builder_readme.md) for more details.

---

### 2. MockBuilder
The `MockBuilder` simplifies the creation, setup, and validation of mock objects, integrating seamlessly with the `ScenarioBuilder`.

#### Example Usage

```csharp
var mockBuilder = new MockBuilder(logger);

mockBuilder.WithMockSetup<IUserService>(m => m.Setup(s => s.GetUser()).Returns(new User { Name = "Test User" }));

mockBuilder.ValidateMock<IUserService>(m => m.Verify(s => s.GetUser(), Times.Once()));
```

Refer to the [MockBuilder Documentation](mockbuilder_documentation.md) for details on advanced features and usage.

---

### 3. Workflow Engine (Planned Feature)

The Workflow Engine will allow developers to execute scenarios step-by-step, validating behaviors in a controlled environment. Steps defined in `ScenarioBuilder` will directly translate into executable workflows.

#### Planned Example:

```csharp
var workflow = SmartMocks.Workflow()
    .StartWith("Initialize API State")
    .Then("Simulate API Request")
    .Then("Validate Response")
    .Run();
```

---

## Logging
SmartMocks uses `Microsoft.Extensions.Logging` to log scenario creation, step additions, and mock interactions. Developers can integrate SmartMocks into their existing logging pipelines for better traceability.

### Example Log Messages:

- *Information*: "Scenario 'User Login Scenario' has been created."
- *Information*: "Given step 'a user is registered' added."
- *Information*: "Mock for type 'IUserService' has been validated."
- *Error*: "Validation error: No mock found for type 'IUserService'."

---

## Example Scenarios

### Simple Scenario

```csharp
var builder = new ScenarioBuilder(logger, "Simple API Test")
    .Given("the API is running")
    .When("a GET request is made to /endpoint")
    .Then("a 200 OK response is returned");

Console.WriteLine(builder.ToGherkin());
```

### Mock Integration with ScenarioBuilder

```csharp
builder
    .Given("a user service is mocked")
    .WithMockSetup<IUserService>(mock => mock.Setup(s => s.GetUser()).Returns(new User { Name = "Mocked User" }))
    .When("the user data is requested")
    .Then("the user data is validated")
    .ValidateMock<IUserService>(mock => mock.Verify(s => s.GetUser(), Times.Once()));
```

---

## Roadmap

1. **ScenarioBuilder**: Fully implemented with Gherkin syntax support.
2. **MockBuilder**: Integrates mocking capabilities for enhanced scenario validation.
3. **Workflow Engine**: Execute and validate scenarios programmatically (Planned).
4. **Enhanced Documentation**: Export scenarios to Markdown or JSON for sharing and collaboration (Planned).

---

## Best Practices

1. **Define Clear Scenarios**:
   - Use `ScenarioBuilder` to articulate test cases with precise `Given`, `When`, and `Then` steps.

2. **Leverage Mocks**:
   - Utilize `MockBuilder` for dependency isolation and validation.

3. **Use Gherkin for Documentation**:
   - Generate Gherkin syntax to document scenarios for BDD workflows.

4. **Enable Logging**:
   - Monitor logs during scenario execution to diagnose issues effectively.

5. **Extend Modularity**:
   - Customize SmartMocks components to suit specific project needs.

---

## Changelog

Current Version: [1.0.0](changelog.md)

