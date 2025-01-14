using Microsoft.Extensions.Logging;
using Moq;
using Workspec.Architecture.SmartMocks.ApplicationModel;

namespace Workspec.Architecture.SmartMocks.Test;

public class ScenarioBuilderTests
{
    private readonly Mock<ILogger> _mockLogger;

    public ScenarioBuilderTests()
    {
        _mockLogger = new Mock<ILogger>();
        _mockLogger.Setup(logger => logger.IsEnabled(It.IsAny<LogLevel>())).Returns(true);
    }

    [Fact]
    public void ScenarioBuilder_ShouldLogScenarioCreation()
    {
        // Arrange
        const string scenarioName = "Test Scenario";

        // Act
        var builder = new ScenarioBuilder(_mockLogger.Object, scenarioName);

        // Assert
        _mockLogger.Verify(
            logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state, type) =>  state.ToString()!.Contains($"Scenario '{scenarioName}' has been created.")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void ScenarioBuilder_Given_ShouldAddStep()
    {
        // Arrange
        var builder = new ScenarioBuilder(_mockLogger.Object, "Test Scenario");

        // Act
        var givenBuilder = builder.Given("a user is logged in");

        // Assert
        Assert.Single(builder.Steps);
        Assert.Equal(ScenarioStepType.Given, builder.Steps[0].Type);
        Assert.Equal("a user is logged in", builder.Steps[0].Description);
    }

    [Fact]
    public void ScenarioBuilder_When_ShouldAddStep()
    {
        // Arrange
        var builder = new ScenarioBuilder(_mockLogger.Object, "Test Scenario");
        var givenBuilder = builder.Given("a user is logged in");

        // Act
        var whenBuilder = givenBuilder.When("the user requests data");

        // Assert
        Assert.Equal(2, builder.Steps.Count);
        Assert.Equal(ScenarioStepType.When, builder.Steps[1].Type);
        Assert.Equal("the user requests data", builder.Steps[1].Description);
    }

    [Fact]
    public void ScenarioBuilder_Then_ShouldAddStep()
    {
        // Arrange
        var builder = new ScenarioBuilder(_mockLogger.Object, "Test Scenario");
        var givenBuilder = builder.Given("a user is logged in").When("the user requests data");

        // Act
        var thenBuilder = givenBuilder.Then("the data is returned");

        // Assert
        Assert.Equal(3, builder.Steps.Count);
        Assert.Equal(ScenarioStepType.Then, builder.Steps[2].Type);
        Assert.Equal("the data is returned", builder.Steps[2].Description);
    }

    [Fact]
    public void ScenarioBuilder_FluentAPI_ShouldAddMultipleSteps()
    {
        // Arrange
        var builder = new ScenarioBuilder(_mockLogger.Object, "Test Scenario");

        // Act
        builder
            .Given("a user is logged in")
            .And("the user has permissions")
            .When("the user requests data")
            .And("the request is valid")
            .Then("the data is returned")
            .And("the request is logged");

        // Assert
        Assert.Equal(6, builder.Steps.Count);
        Assert.Collection(builder.Steps,
            step => Assert.Equal(ScenarioStepType.Given, step.Type),
            step => Assert.Equal(ScenarioStepType.AndGiven, step.Type),
            step => Assert.Equal(ScenarioStepType.When, step.Type),
            step => Assert.Equal(ScenarioStepType.AndWhen, step.Type),
            step => Assert.Equal(ScenarioStepType.Then, step.Type),
            step => Assert.Equal(ScenarioStepType.AndThen, step.Type));
    }

    [Fact]
    public void ScenarioBuilder_ToGherkin_ShouldOutputCorrectSyntax()
    {
        // Arrange
        var builder = new ScenarioBuilder(_mockLogger.Object, "Test Scenario");

        builder
            .Given("a user is logged in")
            .And("the user has permissions")
            .When("the user requests data")
            .Then("the data is returned");

        // Act
        var gherkinOutput = builder.ToGherkin();

        // Assert
        var expectedOutput = @"Scenario: Test Scenario
Given a user is logged in
And the user has permissions
When the user requests data
Then the data is returned";

        Assert.Equal(expectedOutput, gherkinOutput.Trim());
    }

    [Fact]
    public void ScenarioBuilder_ToGherkin_ShouldHandleEmptySteps()
    {
        // Arrange
        var builder = new ScenarioBuilder(_mockLogger.Object, "Empty Scenario");

        // Act
        var gherkinOutput = builder.ToGherkin();

        // Assert
        var expectedOutput = @"Scenario: Empty Scenario";
        Assert.Equal(expectedOutput, gherkinOutput.Trim());
    }

    [Fact]
    public void ScenarioBuilder_ShouldLogStepAddition()
    {
        // Arrange
        var builder = new ScenarioBuilder(_mockLogger.Object, "Test Scenario");

        // Act
        builder.Given("a user is logged in");

        // Assert
        _mockLogger.Verify(
            logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state, type) => state.ToString()!.Contains("Given step 'a user is logged in' added.")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}