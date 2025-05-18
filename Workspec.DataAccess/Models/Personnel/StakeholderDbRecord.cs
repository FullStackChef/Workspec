namespace Workspec.DataAccess.Models.Personnel;

/// <summary>
/// Represents a stakeholder within the system, detailing personal information and their active status.
/// </summary>
/// <param name="Id">The unique identifier for the stakeholder.</param>
/// <param name="TenantId">The tenant identifier under which this stakeholder is registered, supporting multi-tenancy.</param>
/// <param name="FirstName">The first name of the stakeholder.</param>
/// <param name="LastName">The last name of the stakeholder.</param>
/// <param name="Email">The email address of the stakeholder, used for communication and login purposes.</param>
/// <param name="IsActive">A boolean value indicating whether the stakeholder is currently active within the system.</param>
public record StakeholderDbRecord(
    long Id,
    Guid TenantId,
    string FirstName,
    string LastName,
    string Email,
    bool IsActive
);
