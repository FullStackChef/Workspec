namespace Workspec.Odata.Queries.Models;

public record FeatureQueryModel(int Id, string Name, string Description, int ModuleId, string Status, int Priority, string ReleaseVersion)
{
    public virtual ModuleQueryRecord? Module { get; set; }
    public virtual ICollection<UserRequirementQueryModel> UserRequirements { get; set; } = [];
}
