using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Workspec.Domain.Modules.Projects.Models;
using Workspec.Domain.Modules.Projects.Services;

namespace Workspec.Domain.Modules.Projects.API
{
    /// <summary>
    /// Represents the API for managing projects.
    /// </summary>
    public static class ProjectApi
    {
        public const string BaseRoute = "/projects";
        public const string ProjectId = "/{projectId}";

        /// <summary>
        /// Adds project-related services to the application.
        /// </summary>
        /// <param name="builder">The web application builder.</param>
        /// <returns>The web application builder with project services configured.</returns>
        public static WebApplicationBuilder AddProjectServices(this WebApplicationBuilder builder)
        {
            // Add any project-specific services or configurations here
            builder.Services.AddTransient<IProjectService, ProjectService>();

            builder.AddRiskServices();

            return builder;
        }

        /// <summary>
        /// Maps project-related endpoints to the application.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <returns>The web application with project endpoints mapped.</returns>
        public static WebApplication MapProjects(this WebApplication app)
        {
            var groupBuilder = app.MapGroup(BaseRoute);

            // Get all projects
            groupBuilder.MapGet(string.Empty, async ([FromServices] IProjectService projectService) =>
                await projectService.Query().ConfigureAwait(false));

            // Create a new project
            groupBuilder.MapPost(string.Empty, async (
                [FromServices] IProjectService projectService, [FromBody] CreateProjectRecord projectRecord) =>
                await projectService.Create(projectRecord).ConfigureAwait(false));

            // Get a single project by ID
            groupBuilder.MapGet(ProjectId, async (
                [FromServices] IProjectService projectService, [FromRoute] int projectId) =>
                await projectService.Get(projectId).ConfigureAwait(false));

            // Update an existing project
            groupBuilder.MapPut(ProjectId, async (
                [FromServices] IProjectService projectService, int projectId, [FromBody] ProjectUpdateRecord projectRecord) =>
                await projectService.Update(projectId, projectRecord).ConfigureAwait(false));


            app.MapRisks();

            return app;
        }
    }
}
