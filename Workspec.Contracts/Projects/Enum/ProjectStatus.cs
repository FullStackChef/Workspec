namespace Workspec.Contracts.Projects.Enum;

/// <summary>
/// Defines possible statuses for a project, indicating different phases or conditions of the project lifecycle.
/// </summary>
public enum ProjectStatus
{
    Discovery,
    /// <summary>
    /// The project is planned but has not yet started.
    /// </summary>
    Planning,

    /// <summary>
    /// The project is currently active and work is in progress.
    /// </summary>
    Development,

    /// <summary>
    /// The project is temporarily on hold and no active work is being done.
    /// </summary>
    Production,

    /// <summary>
    /// The project has been completed successfully.
    /// </summary>
    Legacy,

    /// <summary>
    /// The project has been cancelled and will not be completed.
    /// </summary>
    Cancelled
}

