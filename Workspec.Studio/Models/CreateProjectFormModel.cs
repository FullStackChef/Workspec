using System.ComponentModel.DataAnnotations;

namespace Workspec.Studio.Models;

public class CreateProjectFormModel
{
    [Required(ErrorMessage = "Product is required.")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Project name is required.")]
    [StringLength(100, ErrorMessage = "Project name must be less than 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Path is required.")]
    [RegularExpression(@"^[a-z0-9\-]+$", ErrorMessage = "Path can only contain lowercase letters, numbers, and hyphens.")]
    [StringLength(100, ErrorMessage = "Path must be less than 100 characters.")]
    public string Path { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description must be less than 500 characters.")]
    public string Description { get; set; } = string.Empty;
}
