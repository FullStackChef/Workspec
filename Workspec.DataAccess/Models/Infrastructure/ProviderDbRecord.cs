namespace Workspec.DataAccess.Models.Infrastructure;

/// <summary>
/// Represents an infrastructure provider, such as a cloud service provider, in Terraform configurations.
/// </summary>
/// <param name="Id">The unique identifier for the provider.</param>
/// <param name="DisplayName">The friendly name used for internal identification of the provider.</param>
/// <param name="TerraformProviderName">The official Terraform provider name as recognized in Terraform configurations (e.g., 'aws', 'azurerm').</param>
/// <param name="DocumentationUrl">The URL to the official documentation of the provider, providing guidelines and configuration details.</param>
public record ProviderDbRecord(int Id, string DisplayName, string TerraformProviderName, string? DocumentationUrl);
