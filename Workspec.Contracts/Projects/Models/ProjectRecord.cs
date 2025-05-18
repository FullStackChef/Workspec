using Workspec.Contracts.Projects.Enum;

namespace Workspec.Contracts.Projects.Models;


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
