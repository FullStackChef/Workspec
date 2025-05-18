namespace Workspec.Odata.Queries.Models;

public record ProductQueryModel(int Id, string Name,string Code, string Description, bool IsActive)
{
    public virtual ICollection<ModuleQueryRecord> Modules { get; set; } = [];

    public virtual ICollection<ProjectQueryModel> Projects { get; set; } = [];
}
