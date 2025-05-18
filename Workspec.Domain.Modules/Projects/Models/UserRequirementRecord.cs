using Workspec.DataAccess.Models.Projects;
using Workspec.Domain.Modules.Projects.Enum;

namespace Workspec.Domain.Modules.Projects.Models;

public record UserRequirementRecord(
    int Id,
    Guid TenantId,
    Guid ObjectId,
    string Title,
    string Description,
    int Priority,
    UserRequirementStatus Status,
    string Version,
    string Tags);

public record CreateUserRequirementRecord(
    string Title,
    string Description,
    int Priority,
    UserRequirementStatus Status,
    string Version,
    string Tags);

public record UserRequirementUpdateRecord(
    string Title,
    string Description,
    int Priority,
    UserRequirementStatus Status,
    string Version,
    string Tags);

public static class UserRequirementExtensions
{
    public static UserRequirementDbRecord ToDbRecord(this CreateUserRequirementRecord record, Guid tenantId) =>
        new(
            default,
            tenantId,
            Guid.NewGuid(),
            record.Title,
            record.Description,
            record.Priority,
            (int)record.Status,
            record.Version,
            record.Tags);

    public static UserRequirementDbRecord UpdateFrom(this UserRequirementDbRecord dbRecord, UserRequirementUpdateRecord updateRecord) =>
        dbRecord with
        {
            Title = updateRecord.Title,
            Description = updateRecord.Description,
            Priority = updateRecord.Priority,
            StatusId = (int)updateRecord.Status,
            Version = updateRecord.Version,
            Tags = updateRecord.Tags
        };

    public static UserRequirementRecord ToModel(this UserRequirementDbRecord dbRecord) =>
        new(
            dbRecord.Id,
            dbRecord.TenantId,
            dbRecord.ObjectId,
            dbRecord.Title,
            dbRecord.Description,
            dbRecord.Priority,
            (UserRequirementStatus)dbRecord.StatusId,
            dbRecord.Version,
            dbRecord.Tags);
}
