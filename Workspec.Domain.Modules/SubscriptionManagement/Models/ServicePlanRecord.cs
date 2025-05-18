namespace Workspec.Domain.Modules.SubscriptionManagement.Models;

/// <summary>
/// Represents a service plan available within the system. This record includes all necessary details
/// to describe a service plan that can be assigned to tenants or users.
/// </summary>
/// <param name="ServicePlanId">The unique identifier for the service plan. This GUID ensures that each service plan can be uniquely identified across the system.</param>
/// <param name="PlanName">The name of the service plan. This name is used to easily identify and reference the plan in user interfaces and reports.</param>
/// <param name="Description">A detailed description of what the service plan includes and any limitations or benefits specific to this plan.</param>
/// <param name="MonthlyCost">The cost of the service plan per month. This value is used for billing purposes and to inform users of the financial requirements associated with this plan.</param>
public record ServicePlanRecord(Guid ServicePlanId, string PlanName, string Description, decimal MonthlyCost);
