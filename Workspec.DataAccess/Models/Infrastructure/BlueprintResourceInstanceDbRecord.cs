namespace Workspec.DataAccess.Models.Infrastructure;

/// <summary>
/// Represents an instance of a resource as defined in a blueprint.
/// </summary>
/// <param name="Id">Unique identifier for the resource instance.</param>
/// <param name="TenantId">Identifier of the tenant to which the resource instance belongs.</param>
/// <param name="BlueprintId">Associated blueprint identifier.</param>
/// <param name="ProviderId">Associated provider identifier.</param>
/// <param name="ResourceId">Associated resource identifier.</param>
/// <param name="InstanceName">The name of the instance as used in configurations.</param>
/// <param name="Description">Description of the resource instance.</param>
public record BlueprintResourceInstanceDbRecord(int Id, Guid TenantId, int BlueprintId, int ProviderId, int ResourceId, string InstanceName, string Description);
