using Workspec.DataAccess.Models.Systems;

namespace Workspec.Domain.Modules.Systems.Models;

/// <summary>
/// Represents an attribute associated with a component type in the system.
/// </summary>
/// <param name="Id">Unique identifier for each component type attribute record.</param>
/// <param name="ComponentTypeId">Identifier for the component type to which this attribute belongs, linking attributes to their respective component types.</param>
/// <param name="Name">Name of the attribute, used for display and identification within the application.</param>
/// <param name="Description">Optional description of the attribute, providing more details about its purpose or usage.</param>
/// <param name="DefaultValue">Default value for the attribute, used when no specific value is provided during configuration.</param>
/// <param name="IsRequired">Indicates whether the attribute is required to be filled for the component type. Defaults to false (0).</param>
public record ComponentTypeAttributeRecord(
    int Id,
    int ComponentTypeId,
    string Name,
    string? Description,
    string? DefaultValue,
    bool IsRequired
);


public static class ComponentTypeAttributeRecordExtensions
{
    /// <summary>
    /// Converts the create component type attribute record to a database record.
    /// </summary>
    /// <param name="record">The create component type attribute record to convert.</param>
    /// <returns>The database record for the component type attribute.</returns>
    public static ComponentTypeAttributeDbRecord ToDbRecord(this ComponentTypeAttributeRecord record) =>
        new(
            record.Id,
            record.ComponentTypeId,
            record.Name,
            record.Description,
            record.DefaultValue,
            record.IsRequired
        );

    /// <summary>
    /// Updates the database component type attribute record based on an update component type attribute record.
    /// </summary>
    /// <param name="record">The database component type attribute record to update.</param>
    /// <param name="updateRecord">The update record containing new values.</param>
    /// <returns>The updated database record.</returns>
    public static ComponentTypeAttributeDbRecord UpdateFrom(this ComponentTypeAttributeDbRecord record, ComponentTypeAttributeRecord updateRecord) =>
        record with
        {
            Name = updateRecord.Name,
            Description = updateRecord.Description,
            DefaultValue = updateRecord.DefaultValue,
            IsRequired = updateRecord.IsRequired
        };

    /// <summary>
    /// Converts the database component type attribute record to a component type attribute model record.
    /// </summary>
    /// <param name="dbRecord">The database component type attribute record to convert.</param>
    /// <returns>The converted component type attribute model record.</returns>
    public static ComponentTypeAttributeRecord ToModel(this ComponentTypeAttributeDbRecord dbRecord) =>
        new(
            dbRecord.Id,
            dbRecord.ComponentTypeId,
            dbRecord.Name,
            dbRecord.Description,
            dbRecord.DefaultValue,
            dbRecord.IsRequired
        );
}
