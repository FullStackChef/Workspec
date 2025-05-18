namespace Workspec.DataAccess.Models.Projects;

public record UserRequirementDbRecord(
    int Id,
    Guid TenantId,
    Guid ObjectId,
    string Title,
    string Description,
    int Priority,
    int StatusId,
    string Version,
    string Tags)
{
    public ICollection<ProjectDbRecord> Projects { get; set; } = [];
}
