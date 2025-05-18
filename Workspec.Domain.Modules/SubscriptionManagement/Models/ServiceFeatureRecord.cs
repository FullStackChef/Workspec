using Workspec.Domain.Modules.SubscriptionManagement.Enum;

namespace Workspec.Domain.Modules.SubscriptionManagement.Models;

/// <summary>
/// Represents a specific feature setting within a service plan. This record details the configuration of a feature,
/// including its limits and whether those limits are enforced as hard or soft.
/// </summary>
/// <param name="ServicePlanId">The identifier of the service plan to which this feature setting applies. This ID links the feature setting
/// directly to a specific service plan, defining the context in which the feature operates.</param>
/// <param name="FeatureType">The type of feature this record represents. Defined by an enum, this parameter specifies
/// what aspect of the service plan's functionality is being configured.</param>
/// <param name="Value">The value assigned to this feature type, typically representing a quantitative limit or setting, such as
/// the number of users, amount of storage, or similar resource metrics associated with the service plan.</param>
/// <param name="HardLimit">A boolean indicating whether the limit specified in the Value should be enforced as a hard limit.
/// True means that exceeding this limit is not permitted under any circumstance, whereas false may allow for temporary or
/// conditional exceeding of the limit.</param>
public record ServiceFeatureRecord(int ServicePlanId, FeatureType FeatureType, int Value, bool HardLimit);
