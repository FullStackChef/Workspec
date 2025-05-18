namespace Workspec.Odata.Queries.Models;

public record RiskQueryModel(
        int Id,
        string Name,
        string Description,
        int ProjectId,
        int RiskClassId,
        string ImpactDescription,
        int ImpactLevel,
        int Probability,
        string MitigationStrategy,
        int ActionId,
        DateTime? Reassess
    );
