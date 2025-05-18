namespace Workspec.DataAccess.Models.Projects;

public record ScenarioDbRecord(
    int Id,
    Guid TenantId,
    Guid ObjectId, string Title);
