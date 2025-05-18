using Microsoft.AspNetCore.Mvc;
using Workspec.Domain.AI.Assistants;
using Workspec.Domain.AI.Models;

namespace workspec_web_app.Server.Endpoints
{
    public static class AiAssistantMappingExtensions
    {
        public static WebApplication MapAiAssistants(this WebApplication app)
        {
            app.MapPost("/api/ai-assistant/project-details",
                async ([FromServices] IProjectAiAssitant aiAssistant, AiAssistantRequest request) =>
            {
                var result = await aiAssistant.GetAiAssistedProjectDetails(request).ConfigureAwait(false);
                return Results.Ok(result);
            });

            return app;
        }
    }
}
