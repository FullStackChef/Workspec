namespace Workspec.DataAccess.Models.Infrastructure;

/// <summary>
/// Represents an environment where resources and services are deployed.
/// </summary>
/// <param name="Id">Unique identifier for the environment.</param>
/// <param name="TenantId">Identifier of the tenant to which the environment belongs.</param>
/// <param name="Name">Name of the environment.</param>
/// <param name="Description">Description of the environment's purpose and characteristics.</param>
public record EnvironmentDbRecord(int Id, Guid TenantId, string Name, string Description);
