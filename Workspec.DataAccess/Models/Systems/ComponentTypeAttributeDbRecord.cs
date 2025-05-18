namespace Workspec.DataAccess.Models.Systems;

/// <summary>
/// Represents an attribute associated with a component type in the system.
/// </summary>
/// <param name="Id">Unique identifier for each component type attribute record.</param>
/// <param name="ComponentTypeId">Identifier for the component type to which this attribute belongs, linking attributes to their respective component types.</param>
/// <param name="Name">Name of the attribute, used for display and identification within the application.</param>
/// <param name="Description">Optional description of the attribute, providing more details about its purpose or usage.</param>
/// <param name="DefaultValue">Default value for the attribute, used when no specific value is provided during configuration.</param>
/// <param name="IsRequired">Indicates whether the attribute is required to be filled for the component type. Defaults to false (0).</param>
public record ComponentTypeAttributeDbRecord(
    int Id,
    int ComponentTypeId,
    string Name,
    string? Description,
    string? DefaultValue,
    bool IsRequired
);
