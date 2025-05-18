using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Workspec.Domain.Modules.Systems.Models;
using Workspec.Domain.Modules.Systems.Services;

namespace Workspec.Domain.Modules.Systems.API
{
    /// <summary>
    /// Represents the API for managing projects.
    /// </summary>
    public static class ComponentTypeApi
    {
        public const string BaseRoute = "/component-types";
        public const string ComponentTypeId = "/{componentTypeId}";

        /// <summary>
        /// Adds project-related services to the application.
        /// </summary>
        /// <param name="builder">The web application builder.</param>
        /// <returns>The web application builder with project services configured.</returns>
        public static WebApplicationBuilder AddComponentTypeServices(this WebApplicationBuilder builder)
        {
            // Add any project-specific services or configurations here
            builder.Services.AddTransient<IComponentTypeService, ComponentTypeService>();

            return builder;
        }

        /// <summary>
        /// Maps project-related endpoints to the application.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <returns>The web application with project endpoints mapped.</returns>
        public static WebApplication MapComponentTypes(this WebApplication app)
        {
            var groupBuilder = app.MapGroup(BaseRoute);

            // Get all projects
            groupBuilder.MapGet(string.Empty, async (
                [FromServices] IComponentTypeService componentTypeService) =>
                await componentTypeService.Query().ConfigureAwait(false));

            // Create a new project
            groupBuilder.MapPost(string.Empty, async (
                [FromServices] IComponentTypeService componentTypeService,
                [FromBody] CreateComponentTypeRecord componentTypeRecord) =>
                await componentTypeService.Create(componentTypeRecord).ConfigureAwait(false));

            // Get a single project by ID
            groupBuilder.MapGet(ComponentTypeId, async (
                [FromServices] IComponentTypeService componentTypeService, [FromRoute] int componentTypeId) =>
                await componentTypeService.Get(componentTypeId).ConfigureAwait(false));

            // Update an existing project
            groupBuilder.MapPut(ComponentTypeId, async (
                [FromServices] IComponentTypeService componentTypeService, int componentTypeId,
                [FromBody] UpdateComponentTypeRecord componentTypeRecord) =>
                await componentTypeService.Update(componentTypeId, componentTypeRecord).ConfigureAwait(false));

            return app;
        }
    }
}
