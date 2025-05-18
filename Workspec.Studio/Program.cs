using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using Workspec.Studio.Components;
using Workspec.Studio.ServiceClients;
using Workspec.Studio.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddScoped<MenuBuilderService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

if (builder.Configuration.GetValue<string>("Syncfusion:LicenseKey") is string licenseKey)
{
    Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);
}

// Add Syncfusion services
builder.Services.AddSyncfusionBlazor();

Action<HttpClient> httpConfig = client => client.BaseAddress = new Uri("http+https://workspec-studio-api");
builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>(httpConfig);
builder.Services.AddHttpClient<IProjectServiceClient, ProjectServiceClient>(httpConfig);

builder.Services.AddScoped<IProjectContextService, ProjectContextService>();
//builder.Services.AddSingleton<MenuBuilderService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
