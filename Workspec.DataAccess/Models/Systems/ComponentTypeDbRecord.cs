namespace Workspec.DataAccess.Models.Systems;

/// <summary>
/// Represents a component type within the system.
/// </summary>
/// <param name="Id">Unique identifier for the component type.</param>
/// <param name="Name">Name of the component type, used for display and selection.</param>
/// <param name="Description">Optional description of the component type, providing more details about its purpose or usage.</param>
public record ComponentTypeDbRecord(int Id, string Name, string? Description)
{
    public ICollection<ComponentTypeAttributeDbRecord> Attributes { get; set; } = [];
}
