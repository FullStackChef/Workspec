using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Workspec.Domain.Modules.Projects.Models;
using Workspec.Domain.Modules.Projects.Services;

namespace Workspec.Domain.Modules.Projects.API;

/// <summary>
/// Represents the API for managing user requirements associated with projects.
/// </summary>
public static class UserRequirementApi
{
    public const string BaseRoute = "/user-requirements";
    public const string RequirementId = "/{requirementId}";

    /// <summary>
    /// Adds user requirement-related services to the application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <returns>The web application builder with user requirement services configured.</returns>
    public static WebApplicationBuilder AddUserRequirementServices(this WebApplicationBuilder builder)
    {
        // Add any user requirement-specific services or configurations here
        builder.Services.AddTransient<IUserRequirementService, UserRequirementService>();
        return builder;
    }

    /// <summary>
    /// Maps user requirement-related endpoints to the application.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>The web application with user requirement endpoints mapped.</returns>
    public static WebApplication MapUserRequirements(this WebApplication app)
    {
        var groupBuilder = app.MapGroup(BaseRoute);

        // Get all user requirements
        groupBuilder.MapGet(string.Empty, async ([FromServices] IUserRequirementService userRequirementService) =>
            await userRequirementService.Query().ConfigureAwait(false));

        // Create a new user requirement
        groupBuilder.MapPost(string.Empty, async (
            [FromServices] IUserRequirementService userRequirementService, [FromBody] CreateUserRequirementRecord requirementRecord) =>
            await userRequirementService.Create(requirementRecord).ConfigureAwait(false));

        // Get a single user requirement by ID
        groupBuilder.MapGet(RequirementId, async (
            [FromServices] IUserRequirementService userRequirementService, [FromRoute] int requirementId) =>
            await userRequirementService.Get(requirementId).ConfigureAwait(false));

        // Update an existing user requirement
        groupBuilder.MapPut(RequirementId, async (
            [FromServices] IUserRequirementService userRequirementService, [FromRoute] int requirementId, [FromBody] UserRequirementUpdateRecord requirementRecord) =>
            await userRequirementService.Update(requirementId, requirementRecord).ConfigureAwait(false));

        return app;
    }
}
