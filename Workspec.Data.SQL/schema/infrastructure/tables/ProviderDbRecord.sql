-- Create ProviderDbRecord table
CREATE TABLE [infrastructure].[ProviderDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [DisplayName] NVARCHAR(255) NOT NULL,  -- Friendly name for internal use
    [TerraformProviderName] NVARCHAR(100) NOT NULL,  -- Official Terraform provider name (e.g., 'aws', 'azurerm')
    [DocumentationUrl] NVARCHAR(255),  -- URL to the provider's official documentation
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [infrastructure].[ProviderDbRecord_History]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each provider.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'ProviderDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Friendly name for internal use to identify the provider.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'ProviderDbRecord',
    @level2type = N'COLUMN', @level2name = 'DisplayName';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The official Terraform provider name as recognized in Terraform configurations.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'ProviderDbRecord',
    @level2type = N'COLUMN', @level2name = 'TerraformProviderName';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'URL to the official documentation of the provider, providing guidelines and configuration details.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'ProviderDbRecord',
    @level2type = N'COLUMN', @level2name = 'DocumentationUrl';
GO
