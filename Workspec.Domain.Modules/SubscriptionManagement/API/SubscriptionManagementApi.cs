using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Workspec.Domain.Modules.SubscriptionManagement.Services;

namespace Workspec.Domain.Modules.SubscriptionManagement.API;

public static class SubscriptionManagementApi
{
    public static WebApplicationBuilder AddSubscriptionManagement(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IFeatureUsageService, FeatureUsageService>();
        builder.Services.AddTransient<ITenantResolutionService, TenantResolutionService>();
        builder.Services.AddTransient<ISubscriptionLimitService, SubscriptionLimitService>();
        return builder;
    }
}
