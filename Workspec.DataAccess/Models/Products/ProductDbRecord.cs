namespace Workspec.DataAccess.Models.Products;

/// <summary>
/// Represents a product within the system, detailing its characteristics and ownership.
/// </summary>
/// <param name="Id">The unique identifier for the product.</param>
/// <param name="TenantId">The tenant identifier under which this product is registered, supporting multi-tenancy.</param>
/// <param name="ObjectId">A globally unique identifier for the product, automatically generated to ensure uniqueness across systems.</param>
/// <param name="Name">The name of the product, providing a clear and concise identifier that is easily recognizable.</param>
/// <param name="Code">A unique code that identifies the product within the system, used for internal reference and identification.</param>
/// <param name="Description">A detailed description of what the product does and its purpose within the organization.</param>
/// <param name="IsActive">A boolean value indicating whether the product is currently active and available.</param>
public record ProductDbRecord(
    int Id,
    Guid TenantId,
    Guid ObjectId,
    string Name,
    string Code,
    string Description,
    bool IsActive
);
