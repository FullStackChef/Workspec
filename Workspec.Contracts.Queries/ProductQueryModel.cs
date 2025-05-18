namespace Workspec.Contracts.Queries;

public record ProductQueryModel(int Id, string Name, string Code, string Description, bool IsActive)
{
    public virtual ICollection<ModuleQueryModel> Modules { get; set; } = [];

    public virtual ICollection<ProjectQueryModel> Projects { get; set; } = [];
}
