namespace Workspec.DataAccess.Models.Systems;

/// <summary>
/// Represents a system within the organization, detailing its characteristics and operational status.
/// </summary>
/// <param name="Id">The unique identifier for the system, primarily used for database indexing and relationships.</param>
/// <param name="TenantId">The tenant identifier under which this system is registered, supporting multi-tenancy.</param>
/// <param name="ObjectId">A globally unique identifier for the system, useful for distributed systems and external integrations.</param>
/// <param name="Name">The name of the system, providing a clear and concise identifier that is easily recognizable.</param>
/// <param name="Description">A detailed description of what the system does and its purpose within the organization.</param>
public record SystemDbRecord(
    int Id,
    Guid TenantId,
    Guid ObjectId,
    string Name,
    string Description
);

