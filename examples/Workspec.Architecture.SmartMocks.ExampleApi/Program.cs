using Workspec.Architecture.SmartMocks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.ForFeature("Feature 1", "features")
    .WithVersion("v1")
    .WithGetEndpoint("{featureId}", endpoint =>
        endpoint.WithScenario("Scenario 1", scenario =>
    {
        scenario
            .Given("A mock is set up")
            .When("The mock is called")
            .Then("Validate the mock was called");
    }));

app.Run();
