using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using OpenAI.Chat;
using System.Security.Claims;
using Workspec.DataAccess.EF;
using Workspec.Domain.AI.Assistants;
using Workspec.Domain.AI.Core;
using Workspec.Domain.Modules.Projects.API;
using Workspec.Domain.Modules.SubscriptionManagement.API;
using Workspec.Domain.Modules.Systems.API;
using Workspec.Odata.Queries;
using workspec_web_app.Server.Endpoints;
using workspec_web_app.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddCors();

builder.AddWorkspaceDataAccess();
builder.AddOdataQueries();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.AddSubscriptionManagement();
builder.AddComponentTypeServices();
builder.AddProjectServices();

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureB2C")
    .EnableTokenAcquisitionToCallDownstreamApi([])
    .AddInMemoryTokenCaches();

builder.Services.Configure<AiAssistantOptions>(builder.Configuration.GetSection("OpenAI"));
builder.Services.AddScoped((IServiceProvider sp) =>
{
    var config = sp.GetRequiredService<IOptions<AiAssistantOptions>>();
    return new ChatClient(config.Value.Model, config.Value.ApiKey);
});
builder.Services.AddScoped<IProjectAiAssitant, ProjectAiAssitant>();

builder.Services.AddSignalR();

//.AddNamedAzureSignalR("signalr");

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseCors(static builder =>
    builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());

app.MapControllers();

app.MapComponentTypes();
app.MapProjects();

app.Map("authentication/login",
    (string? redirectUrl) =>
    Results.Challenge(new AuthenticationProperties
    {
        RedirectUri = redirectUrl ?? "/"
    })
);

app.MapPost("authentication/logout",
    () => Results.SignOut(new AuthenticationProperties
    {
        RedirectUri = "/"
    }, [CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme])
);

app.Map("authentication/user",
    (HttpContext context) =>
    Results.Ok(new UserRecord(
        context.User.Identity?.IsAuthenticated ?? false,
        GivenName: context.User.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
        Surname: context.User.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
        context.User.Claims.Select(c => new { c.Type, c.Value })))
);
app.MapAiAssistants();
app.MapFallbackToFile("/index.html");

app.MapHub<AiAssistantHub>("/ai-assitant");

app.Run();

public record UserRecord(bool IsAuthenticated, string GivenName, string Surname, IEnumerable<object> Claims)
{
    public string? Avatar
    {
        get
        {
            return IsAuthenticated ? $"{GivenName[0]}{Surname[0]}" : null;
        }
    }
}
