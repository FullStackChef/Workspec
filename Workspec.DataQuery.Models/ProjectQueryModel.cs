namespace Workspec.Odata.Queries.Models;

public record ProjectQueryModel(int Id, int ProductId, string Name, string Path, string Description, int Status)
{
    public virtual ProductQueryModel? Product { get; set; }
    public virtual ICollection<UserRequirementQueryModel> UserRequirements { get; set; } = [];
}
