using Microsoft.Extensions.Logging;
using Moq;
using static Workspec.Architecture.SmartMocks.Test.MockBuilderTests;

namespace Workspec.Architecture.SmartMocks.Test;

public class ScenarioBuilderMockBuilderIntegrationTests
{
    private readonly Mock<ILogger<Scenario>> _loggerMock;
    private readonly Scenario _scenarioBuilder;

    public ScenarioBuilderMockBuilderIntegrationTests()
    {
        _loggerMock = new Mock<ILogger<Scenario>>();
        _scenarioBuilder = new Scenario(_loggerMock.Object, "Mock Integration Scenario");
    }

    [Fact]
    public void Given_ShouldAllowMockSetup()
    {
        // Act
        _scenarioBuilder
            .Given("A mock is set up")
            .WithMockSetup<IMyService>(mock => mock.Setup(m => m.DoSomething()).Returns("Mocked Response"));

        var mock = _scenarioBuilder.GetMock<IMyService>();

        // Assert
        Assert.Equal("Mocked Response", mock.Object.DoSomething());
    }

    [Fact]
    public void When_ShouldAllowMockSetup()
    {
        // Act
        _scenarioBuilder
            .Given("A mock is set up")
            .When("The mock is called")
            .WithMockSetup<IMyService>(mock => mock.Setup(m => m.DoSomething()).Returns("Mocked Response"));

        var mock = _scenarioBuilder.GetMock<IMyService>();

        // Assert
        Assert.Equal("Mocked Response", mock.Object.DoSomething());
    }

    [Fact]
    public void Then_ShouldAllowMockValidation()
    {
        // Arrange
        _scenarioBuilder.WithMockSetup<IMyService>(mock => mock.Setup(m => m.DoSomething()));

        var mock = _scenarioBuilder.GetMock<IMyService>();
        mock.Object.DoSomething();

        // Act & Assert
        _scenarioBuilder
            .Given("A mock is set up")
            .When("The mock is called")
            .Then("Validate the mock was called")
            .ValidateMock<IMyService>(mock => mock.Verify(m => m.DoSomething(), Times.Once()));
    }

    [Fact]
    public void WithMockSetup_ShouldDeferSetupUntilMockIsCreated()
    {
        // Act
        _scenarioBuilder.WithMockSetup<IMyService>(mock => mock.Setup(m => m.DoSomething()).Returns("Deferred Setup"));

        var mock = _scenarioBuilder.GetMock<IMyService>();

        // Assert
        Assert.Equal("Deferred Setup", mock.Object.DoSomething());
    }

    [Fact]
    public void ValidateMock_ShouldCreateMock_IfNotExists()
    {
        // Arrange
        var loggerMock = new Mock<ILogger>();
        var mockBuilder = new MockBuilder(loggerMock.Object);

        // Act
        mockBuilder.ValidateMock<IMyService>(mock => mock.Verify(m => m.DoSomething(), Times.Never()));

        // Assert
        var myServiceMock = mockBuilder.GetMock<IMyService>();
        Assert.NotNull(myServiceMock); // Verify the mock was created
        myServiceMock.Verify(m => m.DoSomething(), Times.Never()); // Ensure no interactions have occurred
    }


    [Fact]
    public void ToGherkin_ShouldIncludeMockSetupSteps()
    {
        // Arrange
        _scenarioBuilder
            .Given("A mock is set up")
            .WithMockSetup<IMyService>(mock => mock.Setup(m => m.DoSomething()).Returns("Mocked Response"))
            .When("The mock is called")
            .Then("Validate the mock was called")
            .ValidateMock<IMyService>(mock => mock.Verify(m => m.DoSomething(), Times.Never()));

        // Act
        var gherkinOutput = _scenarioBuilder.ToGherkin();

        // Assert
        Assert.Contains("Given A mock is set up", gherkinOutput);
        Assert.Contains("When The mock is called", gherkinOutput);
        Assert.Contains("Then Validate the mock was called", gherkinOutput);
    }
}

