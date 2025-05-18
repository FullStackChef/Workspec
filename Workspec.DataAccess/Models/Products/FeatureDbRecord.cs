namespace Workspec.DataAccess.Models.Products;

/// <summary>
/// Represents a feature within a module, including details about its implementation and status.
/// </summary>
/// <param name="Id">The unique identifier for the feature.</param>
/// <param name="TenantId">The tenant identifier under which this feature is registered, supporting multi-tenancy.</param>
/// <param name="ModuleId">The identifier of the module to which this feature belongs.</param>
/// <param name="ObjectId">A globally unique identifier for the feature, automatically generated to ensure uniqueness across systems.</param>
/// <param name="Name">The name of the feature, providing a clear and concise identifier that is easily recognizable.</param>
/// <param name="Description">A detailed description of what the feature does and its purpose within the module.</param>
/// <param name="Status">The current status of the feature, such as 'Planned', 'In Development', or 'Completed'.</param>
/// <param name="Priority">The priority level of the feature, indicating its importance relative to other features within the module.</param>
/// <param name="ReleaseVersion">The version of the product release that this feature is associated with or planned for.</param>
public record FeatureDbRecord(
    int Id,
    Guid TenantId,
    int ModuleId,
    Guid ObjectId,
    string Name,
    string Description,
    string Status,
    int Priority,
    string ReleaseVersion
);

