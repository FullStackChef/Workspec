namespace Workspec.Odata.Queries.Models;

public record UserRequirementQueryModel(int Id, int FeatureId, string Title, string Description, int Priority, int StatusId, string Version, string Tags)
{
    public virtual ICollection<ProjectQueryModel> Projects { get; set; } = [];
    public virtual FeatureQueryModel? Feature { get; set; }
}
