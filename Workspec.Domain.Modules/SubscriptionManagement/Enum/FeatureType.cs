namespace Workspec.Domain.Modules.SubscriptionManagement.Enum;

/// <summary>
/// Defines the types of features that can be limited or managed within the workspec SaaS platform.
/// Each feature type corresponds to a specific aspect of the service that can be controlled
/// based on the tenant's subscription plan.
/// </summary>
public enum FeatureType
{
    /// <summary>
    /// Represents the limit on the number of projects a tenant can create.
    /// </summary>
    MaxProjects,

    /// <summary>
    /// Represents the limit on the number of users a tenant can have.
    /// </summary>
    MaxUsers
}
