using Workspec.Domain.Modules.SubscriptionManagement.Enum;

namespace Workspec.Domain.Modules.SubscriptionManagement.Services;

public interface IFeatureUsageService
{
    /// <summary>
    /// Checks if the resource usage, including the proposed increment, remains within the allowed limits of the tenant's subscription plan.
    /// </summary>
    /// <param name="tenantId">The identifier of the tenant.</param>
    /// <param name="feature">The feature for which resource usage is being checked.</param>
    /// <param name="increment">The intended increase in resource usage.</param>
    /// <returns>True if adding the increment does not exceed the resource limits; otherwise, false.</returns>
    Task<bool> IsWithinResourceLimit(Guid tenantId, FeatureType feature, int increment);
}

public class FeatureUsageService : IFeatureUsageService
{
    public async Task<bool> IsWithinResourceLimit(Guid tenantId, FeatureType feature, int increment)
    {
        // Logic to check current usage and limits goes here.
        return await Task.FromResult(true);
    }
}