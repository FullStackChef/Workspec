namespace Workspec.DataAccess.Models.Infrastructure;

/// <summary>
/// Represents a blueprint within the infrastructure, detailing a specific setup or configuration.
/// </summary>
/// <param name="Id">Unique identifier for the blueprint.</param>
/// <param name="TenantId">Identifier of the tenant to which the blueprint belongs.</param>
/// <param name="Name">Name of the blueprint.</param>
/// <param name="Description">Description of what the blueprint entails and its purpose.</param>
public record BlueprintDbRecord(int Id, Guid TenantId, string Name, string Description);
