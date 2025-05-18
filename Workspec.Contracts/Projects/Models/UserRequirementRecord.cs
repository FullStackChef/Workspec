using Workspec.Contracts.Projects.Enum;

namespace Workspec.Contracts.Projects.Models;

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
