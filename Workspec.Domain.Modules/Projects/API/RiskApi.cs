using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Workspec.Domain.Modules.Projects.Models;
using Workspec.Domain.Modules.Projects.Services;

namespace Workspec.Domain.Modules.Projects.API;

/// <summary>
/// Represents the API for managing risks associated with projects.
/// </summary>
public static class RiskApi
{
    public const string BaseRoute = "/risks";
    public const string RiskId = "/{riskId}";

    /// <summary>
    /// Adds risk-related services to the application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <returns>The web application builder with risk services configured.</returns>
    public static WebApplicationBuilder AddRiskServices(this WebApplicationBuilder builder)
    {
        // Add any risk-specific services or configurations here
        builder.Services.AddTransient<IRiskService, RiskService>();
        return builder;
    }

    /// <summary>
    /// Maps risk-related endpoints to the application.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>The web application with risk endpoints mapped.</returns>
    public static WebApplication MapRisks(this WebApplication app)
    {
        var groupBuilder = app.MapGroup(BaseRoute);

        // Get all risks
        groupBuilder.MapGet(string.Empty, async ([FromServices] IRiskService riskService) =>
            await riskService.Query().ConfigureAwait(false));

        // Create a new risk
        groupBuilder.MapPost(string.Empty, async (
            [FromServices] IRiskService riskService, [FromBody] CreateRiskRecord riskRecord) =>
            await riskService.Create(riskRecord).ConfigureAwait(false));

        // Get a single risk by ID
        groupBuilder.MapGet(RiskId, async (
            [FromServices] IRiskService riskService, [FromRoute] int riskId) =>
            await riskService.Get(riskId).ConfigureAwait(false));

        // Update an existing risk
        groupBuilder.MapPut(RiskId, async (
            [FromServices] IRiskService riskService, int riskId, [FromBody] RiskUpdateRecord riskRecord) =>
            await riskService.Update(riskId, riskRecord).ConfigureAwait(false));

        return app;
    }
}
