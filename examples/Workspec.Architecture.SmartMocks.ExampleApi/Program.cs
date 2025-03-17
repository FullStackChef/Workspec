using Workspec.Architecture.SmartMocks;
using Workspec.Architecture.SmartMocks.ExampleApi.Services;
using Workspec.Architecture.SmartMocks.ScenarioBuilder;
using Workspec.Architecture.SmartMocks.ScenarioBuilder.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Register required services
builder.Services.AddSmartMockFeatures(); // Registers FeatureRegistry and FeatureStartupFilter
builder.Services.AddHttpContextAccessor(); // Needed for SmartMock configuration

builder.Services.AddScopedSmartMock<IAuthenticationService>((mock, http) =>
{
    mock.Describe(a => a.Authenticated,
        (val) => $"the user is{(val ? " " : " not ")}authenticated")
        .Returns(http.Request.Headers["X-AUTH"].FirstOrDefault() is not null);
});

var app = builder.Build();

app.ForFeature("Authentication", "authentication")
   .WithVersion("v1")
   .WithScenarios(feature =>
   {
       feature.WithScenario("A user is authenticated",
               scenario => scenario.Given<IAuthenticationService>(service => service.Authenticated, true)
                   .When(ep => ep.Get("authenticated"))
                   .Then(sp => "we return the scenario as gherkin syntax",
                   (HttpContext context, Scenario sc) =>
                   {
                       return ValueTask.FromResult(Results.Ok(sc.ToGherkin()));
                   }));

       feature.WithScenario("A user is not authenticated",
               scenario => scenario
                   .Given<IAuthenticationService>(service => service.Authenticated, false)
                   .When(ep => ep.Get("authenticated"))
                   .Then(sp => "we return the scenario as gherkin syntax",
                   (HttpContext context, Scenario sc) =>
                   {
                       return ValueTask.FromResult(Results.Ok(sc.ToGherkin()));
                   }));
   });

app.Run();

