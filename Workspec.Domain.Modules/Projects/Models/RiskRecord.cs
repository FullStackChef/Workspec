using Workspec.DataAccess.Models.Projects;
using Workspec.Domain.Modules.Projects.Enum;

namespace Workspec.Domain.Modules.Projects.Models;

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

public static class RiskRecordExtensions
{
    /// <summary>
    /// Converts the create risk record to a database record.
    /// </summary>
    /// <param name="record">The create risk record to convert.</param>
    /// <param name="tenantId">The tenant identifier for the risk.</param>
    /// <returns>The database record for the risk.</returns>
    public static RiskDbRecord ToDbRecord(this CreateRiskRecord record, Guid tenantId) =>
       new(
           0,
           tenantId,
           record.ProjectId,
           (int)record.RiskClass,
           record.Description,
           record.ImpactDescription,
           record.ImpactLevel,
           record.Probability,
           record.MitigationStrategy,
           (int)record.Action,
           record.Reassess
        );


    /// <summary>
    /// Updates the database risk record based on an update risk record.
    /// </summary>
    /// <param name="record">The database risk record to update.</param>
    /// <param name="updateRecord">The update record containing new values.</param>
    /// <returns>The updated database record.</returns>
    public static RiskDbRecord UpdateFrom(this RiskDbRecord record, RiskUpdateRecord updateRecord) =>
        record with
        {
            Description = updateRecord.Description,
            ImpactDescription = updateRecord.ImpactDescription,
            ImpactLevel = updateRecord.ImpactLevel,
            Probability = updateRecord.Probability,
            MitigationStrategy = updateRecord.MitigationStrategy,
            ActionId = (int)updateRecord.Action,
            Reassess = updateRecord.Reassess
        };

    /// <summary>
    /// Converts the database risk record to a risk model record.
    /// </summary>
    /// <param name="riskDbRecord">The database risk record to convert.</param>
    /// <returns>The converted risk model record.</returns>
    public static RiskRecord ToModel(this RiskDbRecord riskDbRecord) =>
        new(
            riskDbRecord.Id,
            riskDbRecord.TenantId,
            riskDbRecord.ProjectId,
            (RiskClass)riskDbRecord.RiskClassId,
            riskDbRecord.Description,
            riskDbRecord.ImpactDescription,
            riskDbRecord.ImpactLevel,
            riskDbRecord.Probability,
            riskDbRecord.MitigationStrategy,
            (RiskAction)riskDbRecord.ActionId,
            riskDbRecord.Reassess
        );
}
