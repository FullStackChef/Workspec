# SmartMocks

## Overview
SmartMocks is a .NET framework designed to streamline the process of building, testing, and documenting APIs and workflows. It combines the power of fluent APIs, Gherkin syntax, and modern mocking techniques to simplify early integration testing and ensure accurate documentation.

### Key Features
- **Scenario Builder**: Define test scenarios in a Gherkin-like syntax with a fluent API.
- **Workflow Engine**: Execute defined steps to simulate and test workflows.
- **Mocking Support**: Leverage mocking frameworks like Moq to integrate SmartMocks into your existing test suite.
- **Gherkin Syntax Output**: Export scenarios in Gherkin format for documentation and BDD purposes.

---

## Installation
To install SmartMocks, add the following NuGet package to your project:

```bash
Install-Package Workspec.Architecture.SmartMocks
```

Alternatively, use the .NET CLI:

```bash
dotnet add package Workspec.Architecture.SmartMocks
```

Make sure to include the required dependencies such as `Microsoft.Extensions.Logging` for logging support.

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

### 2. Workflow Engine (Planned Feature)
The Workflow Engine will allow developers to execute scenarios step-by-step, validating behaviors in a controlled environment. Steps defined in `ScenarioBuilder` will directly translate into executable workflows.

---

## Logging
SmartMocks uses `Microsoft.Extensions.Logging` to log scenario creation and step additions. Developers can integrate SmartMocks into their existing logging pipelines for better traceability.

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

### Complex Workflow (Planned Feature)
```csharp
var workflow = SmartMocks.Workflow()
    .StartWith("Initialize API State")
    .Then("Simulate API Request")
    .Then("Validate Response")
    .Run();
```

---

## Roadmap
1. **ScenarioBuilder**: Fully implemented with Gherkin syntax support.
2. **Workflow Engine**: Execute and validate scenarios programmatically.
3. **Integration with Moq**: Build mock APIs directly within SmartMocks.
4. **Enhanced Documentation**: Export scenarios to Markdown or JSON for sharing and collaboration.

---

## Best Practices
- Use `ScenarioBuilder` to define clear and concise scenarios for API behavior.
- Leverage logging to monitor and debug scenarios during development.
- Utilize Gherkin output for documentation and Behavior-Driven Development (BDD).

---

SmartMocks is designed to be modular and extensible, making it a powerful tool for developers aiming to build robust, testable APIs and workflows.

## Changelog

Current Version: [1.0.0](changelog.md)