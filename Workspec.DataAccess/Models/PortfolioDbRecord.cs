namespace Workspec.DataAccess.Models;

public record PortfolioDbRecord(
int Id,
Guid TenantId,
Guid ObjectId,
string Name,
string Description,
Guid OwnerId);
