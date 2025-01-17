using Microsoft.Extensions.Logging;
using Moq;

namespace Workspec.Architecture.SmartMocks;

/// <summary>
/// Provides functionality for creating, setting up, and validating mocks using Moq.
/// </summary>
public class MockBuilder
{
    private readonly ILogger _logger;

    /// <summary>
    /// Stores the created mocks by type.
    /// </summary>
    private readonly Dictionary<Type, object> _mocks = [];

    /// <summary>
    /// Stores setup actions for each mock type.
    /// </summary>
    private readonly Dictionary<Type, List<Action<object>>> _mockSetups = [];

    /// <summary>
    /// Logs when a new mock is created.
    /// </summary>
    private static readonly Action<ILogger, string, Exception?> LogMockCreated =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, nameof(LogMockCreated)),
            "Mock for type '{MockType}' has been created.");

    /// <summary>
    /// Logs when an existing mock is retrieved.
    /// </summary>
    private static readonly Action<ILogger, string, Exception?> LogMockRetrieved =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(2, nameof(LogMockRetrieved)),
            "Existing mock for type '{MockType}' has been retrieved.");

    /// <summary>
    /// Logs when a setup action is registered for a mock type.
    /// </summary>
    private static readonly Action<ILogger, string, Exception?> LogMockSetupRegistered =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(3, nameof(LogMockSetupRegistered)),
            "Setup has been registered for mock of type '{MockType}'.");

    /// <summary>
    /// Logs when a mock is successfully validated.
    /// </summary>
    private static readonly Action<ILogger, string, Exception?> LogMockValidated =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(4, nameof(LogMockValidated)),
            "Mock for type '{MockType}' has been validated.");

    /// <summary>
    /// Logs when a validation error occurs for a mock.
    /// </summary>
    private static readonly Action<ILogger, string, Exception?> LogMockValidationError =
        LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(5, nameof(LogMockValidationError)),
            "Validation error: No mock found for type '{MockType}'.");

    /// <summary>
    /// Initializes a new instance of the <see cref="MockBuilder"/> class.
    /// </summary>
    /// <param name="logger">The logger to use for logging actions and events.</param>
    public MockBuilder(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logger.LogInformation("MockBuilder initialized.");
    }

    /// <summary>
    /// Retrieves the mock object for the specified type, creating it if it does not already exist.
    /// </summary>
    /// <typeparam name="T">The type of the mock object to retrieve.</typeparam>
    /// <returns>The mock object of type <typeparamref name="T"/>.</returns>
    public Mock<T> GetMock<T>() where T : class => GetMock<T>(typeof(T).Name);

    /// <summary>
    /// Retrieves the mock object for the specified type, creating it if it does not already exist.
    /// Allows specifying a custom type name for logging purposes.
    /// </summary>
    /// <typeparam name="T">The type of the mock object to retrieve.</typeparam>
    /// <param name="typeName">The name of the type for logging purposes.</param>
    /// <returns>The mock object of type <typeparamref name="T"/>.</returns>
    public Mock<T> GetMock<T>(string typeName) where T : class
    {
        if (!_mocks.TryGetValue(typeof(T), out var mock))
        {
            var newMock = new Mock<T>();
            _mocks[typeof(T)] = newMock;

            LogMockCreated(_logger, typeName, null);

            if (_mockSetups.TryGetValue(typeof(T), out var setups))
            {
                foreach (var setup in setups)
                {
                    setup(newMock);
                }
            }

            return newMock;
        }

        LogMockRetrieved(_logger, typeName, null);
        return (Mock<T>)mock;
    }

    /// <summary>
    /// Registers a setup action for the specified mock type. 
    /// If the mock already exists, the setup is applied immediately.
    /// </summary>
    /// <typeparam name="T">The type of the mock object to set up.</typeparam>
    /// <param name="setup">The setup action to apply to the mock.</param>
    /// <returns>The current <see cref="MockBuilder"/> instance for chaining.</returns>
    public MockBuilder WithMockSetup<T>(Action<Mock<T>> setup) where T : class
    {
        var typeName = typeof(T).Name;

        if (!_mockSetups.TryGetValue(typeof(T), out var setups))
        {
            setups = [];
            _mockSetups[typeof(T)] = setups;
        }

        setups.Add(mock => setup((Mock<T>)mock));

        if (_mocks.TryGetValue(typeof(T), out var existingMock))
        {
            setup((Mock<T>)existingMock);
        }

        LogMockSetupRegistered(_logger, typeName, null);
        return this;
    }

    /// <summary>
    /// Validates interactions with the specified mock object.
    /// If the mock does not exist, it is created and all registered setups are applied before validation.
    /// </summary>
    /// <typeparam name="T">The type of the mock object to validate.</typeparam>
    /// <param name="validation">The validation action to apply to the mock.</param>
    /// <returns>The current <see cref="MockBuilder"/> instance for chaining.</returns>
    public MockBuilder ValidateMock<T>(Action<Mock<T>> validation) where T : class
    {
        var typeName = typeof(T).Name;
        try
        {
            validation(GetMock<T>());
            LogMockValidated(_logger, typeName, null);
        }
        catch (Exception ex)
        {
            LogMockValidationError(_logger, typeName, ex);
            throw;
        }
        return this;
    }
}
