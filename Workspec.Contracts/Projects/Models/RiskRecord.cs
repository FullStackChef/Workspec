using Workspec.Contracts.Projects.Enum;

namespace Workspec.Contracts.Projects.Models;

/// <summary>
/// Represents a risk associated with a specific project, encapsulating details about the risk's nature, impact, and mitigation strategies.
/// </summary>
/// <param name="Id">The unique identifier for the risk record.</param>
/// <param name="TenantId">The identifier linking the risk to a specific tenant.</param>
/// <param name="ProjectId">The identifier of the project this risk is associated with.</param>
/// <param name="RiskClass">The identifier for the risk classification, which helps categorize the risk.</param>
/// <param name="Description">A brief description of the risk.</param>
/// <param name="ImpactDescription">A description of the potential impact of the risk on the project.</param>
/// <param name="ImpactLevel">The level of impact of the risk, usually quantified by severity.</param>
/// <param name="Probability">The probability of the risk occurring, typically expressed as a percentage.</param>
/// <param name="MitigationStrategy">The strategy planned to mitigate the risk.</param>
/// <param name="ActionId">The identifier for the action taken to mitigate the risk.</param>
/// <param name="Reassess">The optional date for when the risk should be reassessed.</param>
public record RiskRecord(
    int Id,
    Guid TenantId,
    int ProjectId,
    RiskClass RiskClass,
    string Description,
    string ImpactDescription,
    int ImpactLevel,
    int Probability,
    string MitigationStrategy,
    RiskAction Action,
    DateTime? Reassess
);

public record CreateRiskRecord(
    int ProjectId,
    RiskClass RiskClass,
    string Description,
    string ImpactDescription,
    int ImpactLevel,
    int Probability,
    string MitigationStrategy,
    RiskAction Action,
    DateTime? Reassess
);

public record RiskUpdateRecord(
    string Description,
    string ImpactDescription,
    int ImpactLevel,
    int Probability,
    string MitigationStrategy,
    RiskAction Action,
    DateTime? Reassess
);
