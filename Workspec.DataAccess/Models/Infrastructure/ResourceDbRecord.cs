namespace Workspec.DataAccess.Models.Infrastructure;

/// <summary>
/// Represents a resource that is part of the infrastructure.
/// </summary>
/// <param name="Id">The unique identifier for the resource.</param>
/// <param name="ResourceTypeId">The identifier for the type of resource, linking to the ResourceType table.</param>
/// <param name="Name">The name of the resource.</param>
/// <param name="Description">A detailed description of the resource and its usage.</param>
/// <param name="Status">The current status of the resource, represented as an integer code.</param>
/// <param name="TenantId">The identifier of the tenant to which the resource belongs.</param>
public record ResourceDbRecord(int Id, int ResourceTypeId, string Name, string Description, int Status, Guid TenantId);
