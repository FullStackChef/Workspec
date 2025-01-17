using Microsoft.Extensions.Logging;
using Moq;

namespace Workspec.Architecture.SmartMocks.Test;

public class MockBuilderTests
{
    private readonly Mock<ILogger> _loggerMock;
    private readonly MockBuilder _mockBuilder;

    public MockBuilderTests()
    {
        _loggerMock = new Mock<ILogger>();
        _loggerMock.Setup(l => l.IsEnabled(It.IsAny<LogLevel>())).Returns(true);
        _mockBuilder = new MockBuilder(_loggerMock.Object);
    }

    [Fact]
    public void GetMock_ShouldCreateNewMock_WhenMockDoesNotExist()
    {
        // Act
        var mock = _mockBuilder.GetMock<IMyService>();

        // Assert
        Assert.NotNull(mock);
        _loggerMock.VerifyLog(LogLevel.Information, "Mock for type 'IMyService' has been created.", Times.Once());
    }

    [Fact]
    public void GetMock_ShouldReturnExistingMock_WhenMockAlreadyExists()
    {
        // Arrange
        var firstMock = _mockBuilder.GetMock<IMyService>();

        // Act
        var secondMock = _mockBuilder.GetMock<IMyService>();

        // Assert
        Assert.Same(firstMock, secondMock);
        _loggerMock.VerifyLog(LogLevel.Information, "Mock for type 'IMyService' has been created.", Times.Once());
        _loggerMock.VerifyLog(LogLevel.Information, "Existing mock for type 'IMyService' has been retrieved.", Times.Once());
    }

    [Fact]
    public void WithMockSetup_ShouldApplySetupImmediately_WhenMockExists()
    {
        // Arrange
        var mock = _mockBuilder.GetMock<IMyService>();

        // Act
        _mockBuilder.WithMockSetup<IMyService>(m => m.Setup(s => s.DoSomething()).Returns("Mocked Value"));

        // Assert
        Assert.Equal("Mocked Value", mock.Object.DoSomething());
        _loggerMock.VerifyLog(LogLevel.Information, "Setup has been registered for mock of type 'IMyService'.", Times.Once());
    }

    [Fact]
    public void WithMockSetup_ShouldDeferSetup_WhenMockDoesNotExist()
    {
        // Act
        _mockBuilder.WithMockSetup<IMyService>(m => m.Setup(s => s.DoSomething()).Returns("Mocked Value"));
        var mock = _mockBuilder.GetMock<IMyService>();

        // Assert
        Assert.Equal("Mocked Value", mock.Object.DoSomething());
        _loggerMock.VerifyLog(LogLevel.Information, "Setup has been registered for mock of type 'IMyService'.", Times.Once());
    }

    [Fact]
    public void ValidateMock_ShouldValidateMockInteractions()
    {
        // Arrange
        var mock = _mockBuilder.GetMock<IMyService>();
        mock.Object.DoSomething();

        // Act
        _mockBuilder.ValidateMock<IMyService>(m => m.Verify(s => s.DoSomething(), Times.Once()));

        // Assert
        _loggerMock.VerifyLog(LogLevel.Information, "Mock for type 'IMyService' has been validated.", Times.Once());
    }

    [Fact]
    public void ValidateMock_ShouldCreateAndApplySetups_WhenMockDoesNotExist()
    {
        // Arrange
        _mockBuilder.WithMockSetup<IMyService>(m => m.Setup(s => s.DoSomething()).Returns("Deferred Setup"));

        // Act
        _mockBuilder.ValidateMock<IMyService>(m => m.Verify(s => s.DoSomething(), Times.Never()));

        // Assert
        var mock = _mockBuilder.GetMock<IMyService>();
        Assert.NotNull(mock);
        Assert.Equal("Deferred Setup", mock.Object.DoSomething());
        _loggerMock.VerifyLog(LogLevel.Information, "Mock for type 'IMyService' has been validated.", Times.Once());
        _loggerMock.VerifyLog(LogLevel.Information, "Mock for type 'IMyService' has been created.", Times.Once());
    }

    public interface IMyService
    {
        string DoSomething();
    }
}
