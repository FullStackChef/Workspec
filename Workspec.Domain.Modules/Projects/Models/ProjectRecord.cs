using Workspec.DataAccess.Models.Projects;
using Workspec.Domain.Modules.Projects.Enum;

namespace Workspec.Domain.Modules.Projects.Models;


/// <summary>
/// Represents a project with detailed management information.
/// </summary>
/// <param name="Id">An integer identifier for the project, used for database indexing and relationships.</param>
/// <param name="ObjectId">A globally unique identifier for the project, useful for distributed systems and external integrations.</param>
/// <param name="Name">The name of the project.</param>
/// <param name="Description">A detailed description of the project's goals and scope.</param>
/// <param name="OwnerId">The identifier of the user who owns or manages the project.</param>
/// <param name="Status">The current status of the project, indicating its phase or condition.</param>
public record ProjectRecord(
    int Id,
    Guid ObjectId,
    int ProductId,
    string Name,
    string Path,
    string Description,
    ProjectStatus Status
);

public record CreateProjectRecord(int ProductId, string Name, string Path, string Description);

public record ProjectUpdateRecord(string Description, ProjectStatus Status);

public static class ProjectRecordExtensions
{
    /// <summary>
    /// Updates the status of the project record.
    /// </summary>
    /// <param name="record">The project record to update.</param>
    /// <param name="status">The new status of the project.</param>
    /// <returns>The updated project record with the new status.</returns>
    public static ProjectRecord WithStatus(this ProjectRecord record, ProjectStatus status) =>
        record with { Status = status };

    public static ProjectDbRecord UpdateFrom(this ProjectDbRecord record, ProjectUpdateRecord updateRecord) =>
       record with { Description = updateRecord.Description, Status = (int)updateRecord.Status };

    public static ProjectDbRecord ToDbRecord(this CreateProjectRecord record, Guid tenantId) =>
        new(
            default,
            tenantId,
            Guid.NewGuid(),
            record.ProductId,
            record.Name,
            record.Path,
            record.Description,
            (int)ProjectStatus.Discovery
        );

    /// <summary>
    /// Converts the project record to a database record.
    /// </summary>
    /// <param name="record">The project record to convert.</param>
    /// <param name="tenantId">The identifier of the tenant.</param>
    /// <returns>The converted database record.</returns>
    public static ProjectDbRecord ToDbRecord(this ProjectRecord record, Guid tenantId) =>
        new(
            record.Id,
            tenantId,
            record.ObjectId,
            record.ProductId,
            record.Name,
            record.Path,
            record.Description,
            (int)record.Status
        );

    /// <summary>
    /// Converts the database record to a project record.
    /// </summary>
    /// <param name="projectDbRecord">The database record to convert.</param>
    /// <returns>The converted project record.</returns>
    public static ProjectRecord ToModel(this ProjectDbRecord projectDbRecord) =>
        new(
            projectDbRecord.Id,
            projectDbRecord.ObjectId,
            projectDbRecord.ProductId,
            projectDbRecord.Name,
            projectDbRecord.Path,
            projectDbRecord.Description,
            (ProjectStatus)projectDbRecord.Status
        );
}
