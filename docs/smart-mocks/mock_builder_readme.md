# MockBuilder: User Documentation

The `MockBuilder` is a utility for creating, setting up, and validating mock objects in a structured and efficient manner. It leverages the Moq framework to simplify the process of managing mocks in your tests.

## Features

1. **Mock Creation**:
   - Automatically creates mocks when needed.
   - Ensures that only one instance of each mock is created.

2. **Deferred Setup**:
   - Allows setups to be registered before the mock is created.
   - Automatically applies setups when the mock is retrieved or validated.

3. **Validation**:
   - Provides an easy way to validate mock interactions.
   - Creates the mock and applies setups if it doesn’t already exist.

4. **Structured Logging**:
   - Logs important events like mock creation, retrieval, setup registration, and validation for better debugging and test transparency.

---

## Getting Started

### Initialization

To use `MockBuilder`, you need an instance of a logger that implements `ILogger`.

```csharp
using Microsoft.Extensions.Logging;
using Workspec.Architecture.SmartMocks;

var logger = new LoggerFactory().CreateLogger<MockBuilder>();
var mockBuilder = new MockBuilder(logger);
```

### Creating and Retrieving Mocks

You can retrieve a mock using the `GetMock<T>()` method. If the mock doesn’t already exist, it will be created automatically.

```csharp
var myServiceMock = mockBuilder.GetMock<IMyService>();

// Use the mock
myServiceMock.Object.DoSomething();
```

---

## Methods

### `GetMock<T>()`

Retrieves the mock object for the specified type. Creates a new mock if it doesn’t already exist.

#### Example:

```csharp
var mock = mockBuilder.GetMock<IMyService>();
mock.Object.DoSomething();
```

### `WithMockSetup<T>(Action<Mock<T>> setup)`

Registers a setup action for the specified mock type. If the mock already exists, the setup is applied immediately. Otherwise, the setup is stored and applied when the mock is created or validated.

#### Example:

```csharp
mockBuilder.WithMockSetup<IMyService>(m => m.Setup(s => s.DoSomething()).Returns("Hello, World!"));

var mock = mockBuilder.GetMock<IMyService>();
Console.WriteLine(mock.Object.DoSomething()); // Outputs: "Hello, World!"
```

### `ValidateMock<T>(Action<Mock<T>> validation)`

Validates interactions with the specified mock object. If the mock doesn’t exist, it is created and all registered setups are applied before validation.

#### Example:

```csharp
mockBuilder.ValidateMock<IMyService>(m => m.Verify(s => s.DoSomething(), Times.Once()));
```

---

## Logging

`MockBuilder` uses structured logging to provide insights into its operations. These are the key log events:

- **Mock Created**: Logged when a new mock is created.
- **Mock Retrieved**: Logged when an existing mock is retrieved.
- **Setup Registered**: Logged when a setup action is registered for a mock.
- **Mock Validated**: Logged when a mock is validated.
- **Validation Error**: Logged when validation fails.

### Example Log Messages:

- *Information*: “Mock for type 'IMyService' has been created.”
- *Information*: “Existing mock for type 'IMyService' has been retrieved.”
- *Information*: “Setup has been registered for mock of type 'IMyService'.”
- *Information*: “Mock for type 'IMyService' has been validated.”
- *Error*: “Validation error: No mock found for type 'IMyService'.”

---

## Best Practices

1. **Register Setups Early**:
   - Use `WithMockSetup` before the mock is retrieved or validated to ensure all setups are applied correctly.

2. **Validate in Tests**:
   - Always use `ValidateMock` in tests to confirm expected interactions with your mocks.

3. **Use Logging**:
   - Enable logging to troubleshoot issues during testing.

---

## Advanced Scenarios

### Deferred Setup and Validation

Deferred setups allow you to define behaviors even if the mock hasn’t been created yet. These setups will automatically be applied when the mock is created or validated.

```csharp
mockBuilder.WithMockSetup<IMyService>(m => m.Setup(s => s.DoSomething()).Returns("Deferred Value"));

// Validate the mock
mockBuilder.ValidateMock<IMyService>(m => m.Verify(s => s.DoSomething(), Times.Never()));

var mock = mockBuilder.GetMock<IMyService>();
Console.WriteLine(mock.Object.DoSomething()); // Outputs: "Deferred Value"
```

---

## FAQs

### What happens if I call `ValidateMock` on a mock that doesn’t exist?
The mock will be created, any registered setups will be applied, and then validation will proceed.

### Can I use `MockBuilder` without registering setups?
Yes, you can use `GetMock` and `ValidateMock` independently. Setups are optional but recommended for defining behaviors.

### Does `MockBuilder` support multiple setups for the same mock type?
Yes, you can register multiple setups for the same mock. All setups will be applied in the order they are registered.

---

## Conclusion

`MockBuilder` simplifies the process of managing mocks in your tests by combining mock creation, setup, and validation into a single utility. With deferred setup, structured logging, and lazy mock creation, it’s a powerful tool for creating maintainable and effective tests.

For more advanced use cases or troubleshooting, consult the detailed logs or extend the utility to fit your needs.

