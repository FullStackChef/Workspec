namespace Workspec.DataAccess.Models.Projects;

public record StepDbRecord(int Id, Guid TenantId, Guid ObjectId, string Keyword, string Text);
