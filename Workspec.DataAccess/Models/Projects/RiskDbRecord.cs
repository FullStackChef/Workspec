namespace Workspec.DataAccess.Models.Projects;

public record RiskDbRecord(
    int Id,
    Guid TenantId,
    int ProjectId,
    int RiskClassId,
    string Description,
    string ImpactDescription,
    int ImpactLevel,
    int Probability,
    string MitigationStrategy,
    int ActionId,
    DateTime? Reassess
);
