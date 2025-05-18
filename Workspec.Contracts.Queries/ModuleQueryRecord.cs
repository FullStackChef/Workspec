namespace Workspec.Contracts.Queries;

public record ModuleQueryModel(int Id, string Name, string Description, int ProductId)
{
    public virtual ProductQueryModel? Product { get; set; }
    public virtual ICollection<FeatureQueryModel> Features { get; set; } = [];
}
