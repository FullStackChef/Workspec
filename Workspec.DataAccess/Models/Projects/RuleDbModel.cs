namespace Workspec.DataAccess.Models.Projects;

public record RuleDbRecord(
    int Id,
    Guid TenantId,
    Guid ObjectId,
    string Title
);

