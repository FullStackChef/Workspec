using Workspec.DataAccess.Models.Systems;

namespace Workspec.Domain.Modules.Systems.Models;

/// <summary>
/// Represents a component type within the system.
/// </summary>
/// <param name="Id">Unique identifier for the component type.</param>
/// <param name="Name">Name of the component type, used for display and selection.</param>
/// <param name="Description">Optional description of the component type, providing more details about its purpose or usage.</param>
/// <param name="Attributes">List of attributes associated with the component type.</param>
public record ComponentTypeRecord(
    int Id,
    string Name,
    string? Description,
    List<ComponentTypeAttributeRecord> Attributes
);

/// <summary>
/// Represents the data needed to create a new component type.
/// </summary>
/// <param name="Name">Name of the component type.</param>
/// <param name="Description">Optional description of the component type.</param>
/// <param name="Attributes">List of attributes to be associated with the component type.</param>
public record CreateComponentTypeRecord(
    string Name,
    string? Description,
    List<ComponentTypeAttributeRecord> Attributes
);

/// <summary>
/// Represents the data needed to update an existing component type.
/// </summary>
/// <param name="Name">Name of the component type.</param>
/// <param name="Description">Optional description of the component type.</param>
public record UpdateComponentTypeRecord(
    string Name,
    string? Description,
    List<ComponentTypeAttributeRecord> Attributes
);

public static class ComponentTypeRecordExtensions
{
    /// <summary>
    /// Converts the create component type record to a database record.
    /// </summary>
    /// <param name="record">The create component type record to convert.</param>
    /// <param name="tenantId">The tenant identifier for the component type.</param>
    /// <returns>The database record for the component type.</returns>
    public static ComponentTypeDbRecord ToDbRecord(this CreateComponentTypeRecord record, Guid tenantId) =>
        new(
            default,
            record.Name,
            record.Description
        )
        {
            Attributes = record.Attributes.Select(a => a.ToDbRecord()).ToList()
        };

    /// <summary>
    /// Updates the database component type record based on an update component type record.
    /// </summary>
    /// <param name="record">The database component type record to update.</param>
    /// <param name="updateRecord">The update record containing new values.</param>
    /// <returns>The updated database record.</returns>
    public static ComponentTypeDbRecord UpdateFrom(this ComponentTypeDbRecord record, UpdateComponentTypeRecord updateRecord) =>
        record with
        {
            Name = updateRecord.Name,
            Description = updateRecord.Description,
            Attributes = updateRecord.Attributes.Select(a =>
            {
                return a.Id != default &&
                record.Attributes.FirstOrDefault(i =>
                    i.Id == a.Id) is ComponentTypeAttributeDbRecord existing ?
                    existing.UpdateFrom(a) : a.ToDbRecord();
            }).ToList()
        };

    /// <summary>
    /// Converts the database component type record to a component type model record.
    /// </summary>
    /// <param name="dbRecord">The database component type record to convert.</param>
    /// <returns>The converted component type model record.</returns>
    public static ComponentTypeRecord ToModel(this ComponentTypeDbRecord dbRecord) =>
        new(
            dbRecord.Id,
            dbRecord.Name,
            dbRecord.Description,
            dbRecord.Attributes.Select(a => a.ToModel()).ToList()
        );
}
