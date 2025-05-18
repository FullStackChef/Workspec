namespace Workspec.DataAccess.Models.Projects;

/// <summary>
/// Represents a project with detailed management information. This record is used for
/// database operations related to projects in a multi-tenant environment.
/// </summary>
/// <param name="Id">An integer identifier for the project, primarily used for database indexing and relationships.</param>
/// <param name="TenantId">The identifier of the tenant under which this project is registered, supporting multi-tenancy by linking projects to specific tenants.</param>
/// <param name="ObjectId">A globally unique identifier for the project, ensuring uniqueness across distributed systems and external integrations.</param>
/// <param name="Name">The name of the project, which provides a brief but descriptive identification for the project.</param>
/// <param name="Description">A detailed description of the project's goals and scope, offering insights into the project’s objectives and strategic alignment.</param>
/// <param name="Status">An integer representation of the current status of the project, which denotes its phase or condition in the project lifecycle.</param>
public record ProjectDbRecord(
    int Id,
    Guid TenantId,
    Guid ObjectId,
    int ProductId,
    string Name,
    string Path,
    string Description,
    int Status
)
{
    public ICollection<UserRequirementDbRecord> UserRequirements { get; set; } = [];
}
