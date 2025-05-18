namespace Workspec.Contracts.Queries;

public record FeatureQueryModel(int Id, string Name, string Description, int ModuleId, string Status, int Priority, string ReleaseVersion)
{
    public virtual ModuleQueryModel? Module { get; set; }
    public virtual ICollection<UserRequirementQueryModel> UserRequirements { get; set; } = [];
}
