namespace Workspec.DataAccess.Models.Products;

/// <summary>
/// Represents a module within a product, detailing its characteristics and linkage to the product.
/// </summary>
/// <param name="Id">The unique identifier for the module.</param>
/// <param name="TenantId">The tenant identifier under which this module is registered, supporting multi-tenancy.</param>
/// <param name="ProductId">The identifier of the product to which this module belongs.</param>
/// <param name="ObjectId">A globally unique identifier for the module, automatically generated to ensure uniqueness across systems.</param>
/// <param name="Name">The name of the module, providing a clear and concise identifier that is easily recognizable.</param>
/// <param name="Description">A detailed description of what the module does and its purpose within the product.</param>
public record ModuleDbRecord(
    int Id,
    Guid TenantId,
    int ProductId,
    Guid ObjectId,
    string Name,
    string Description
);

