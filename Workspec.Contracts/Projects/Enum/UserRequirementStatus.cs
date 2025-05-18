namespace Workspec.Contracts.Projects.Enum;

public enum UserRequirementStatus
{
    Draft,         // Initial state, requirement has not been validated or approved
    Validated,     // Requirement has been checked and validated but not yet approved
    Approved,      // Requirement has received final approval and is ready for implementation
    InProgress,    // Work on the requirement has started
    OnHold,        // Requirement implementation is paused
    Completed,     // Requirement has been fully implemented
    Rejected       // Requirement has been rejected at any stage (validation or approval)
}
