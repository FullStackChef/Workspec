using Workspec.Domain.Modules.SubscriptionManagement.Enum;

namespace Workspec.Domain.Modules.SubscriptionManagement.Models;

/// <summary>
/// Represents a type of feature available in the service plans. This record includes key details
/// about different features that can be enabled or restricted within a service plan.
/// </summary>
/// <param name="FeatureType">The enumeration representing the unique identifier for the feature type.
/// This is used to uniquely identify and manage feature types within the system's database and logic.</param>
/// <param name="Name">The name of the feature type. This is used to succinctly describe the feature,
/// facilitating easier recognition and reference in both UI and backend processes.</param>
/// <param name="Description">A detailed description of the feature type, explaining what the feature does
/// and how it impacts the functionality available to the users or tenants of a service plan.</param>
public record FeatureTypeRecord(FeatureType FeatureType, string Name, string Description);
