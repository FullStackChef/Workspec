namespace Workspec.DataAccess.Models.Systems;

/// <summary>
/// Represents a component within a system, detailing its characteristics and linkage to the system.
/// </summary>
/// <param name="Id">The unique identifier for the component.</param>
/// <param name="SystemId">The identifier of the system to which this component belongs.</param>
/// <param name="ObjectId">A globally unique identifier for the component, useful for distributed systems and external integrations.</param>
/// <param name="Name">The name of the component, providing a clear and concise identifier that is easily recognizable.</param>
/// <param name="Description">A detailed description of what the component does and its purpose within the system.</param>
/// <param name="TenantId">The tenant identifier under which this component is registered, supporting multi-tenancy.</param>
public record ComponentDbRecord(
    int Id,
    int SystemId,
    Guid ObjectId,
    string Name,
    string Description,
    Guid TenantId
);

