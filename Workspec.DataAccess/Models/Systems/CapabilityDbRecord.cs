namespace Workspec.DataAccess.Models.Systems;

/// <summary>
/// Represents a capability within a module, detailing its characteristics and linkage to the module.
/// </summary>
/// <param name="Id">The unique identifier for the capability.</param>
/// <param name="ModuleId">The identifier of the module to which this capability belongs.</param>
/// <param name="ObjectId">A globally unique identifier for the capability, useful for distributed systems and external integrations.</param>
/// <param name="Name">The name of the capability, providing a clear and concise identifier that is easily recognizable.</param>
/// <param name="Description">A detailed description of what the capability does and its purpose within the module.</param>
/// <param name="TenantId">The tenant identifier under which this capability is registered, supporting multi-tenancy.</param>
public record CapabilityDbRecord(
    int Id,
    int ModuleId,
    Guid ObjectId,
    string Name,
    string Description,
    Guid TenantId
);

