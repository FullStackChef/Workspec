namespace Workspec.Studio.Models;

public class RequirementNode
{
    public string Id { get; set; }           // Unique ID for TreeView/TreeGrid
    public string? ParentId { get; set; }     // Null for top-level modules
    public string Title { get; set; }        // Name to display
    public string Description { get; set; }  // Optional description
    public string Type { get; set; }         // "Module", "Feature", or "Requirement"

    public bool HasChildren { get; set; }
    public List<RequirementNode> Children { get; set; } = [];
}