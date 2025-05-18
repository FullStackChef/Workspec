using Microsoft.AspNetCore.Http;
using Workspec.Domain.Modules.SubscriptionManagement.Enum;

namespace Workspec.Domain.Modules.SubscriptionManagement.Services;

/// <summary>
/// Provides an interface for checking if specific resource usage limits have been reached within a tenant's subscription plan.
/// </summary>
public interface ISubscriptionLimitService
{
    /// <summary>
    /// Checks if adding a specified increment to a resource usage for a given feature will exceed the subscription limits.
    /// </summary>
    /// <param name="tenantId">The unique identifier for the tenant whose limits are being checked.</param>
    /// <param name="featureType">The type of feature for which usage limits are being checked.</param>
    /// <param name="increment">The amount by which the feature's usage is proposed to be increased.</param>
    /// <returns>A task that returns an <see cref="IResult"/> indicating whether the operation would exceed limits.</returns>
    Task<IResult> CheckLimitAsync(Guid tenantId, FeatureType featureType, int increment);
}

/// <summary>
/// Implements the subscription limit checking service, determining if specified resource usage is within the allowed limits.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SubscriptionLimitService"/> class.
/// </remarks>
/// <param name="usageService">The service used to check current usage and limits of features.</param>
public class SubscriptionLimitService(IFeatureUsageService usageService) : ISubscriptionLimitService
{

    /// <inheritdoc/>
    public async Task<IResult> CheckLimitAsync(Guid tenantId, FeatureType featureType, int increment)
    {
        var withinLimits = await usageService.IsWithinResourceLimit(tenantId, featureType, increment);

        if (withinLimits)
        {
            return Results.Ok();
        }

        return Results.Problem(new()
        {
            Title = "Feature Limit Reached",
            Detail = "You have reached your limit for this feature. Please upgrade your plan or reduce your usage.",
            Status = StatusCodes.Status403Forbidden,
            Type = "https://example.com/feature-limit-reached",
            Extensions =
            {
                ["featureType"] = featureType.ToString()
            }
        });
    }
}
