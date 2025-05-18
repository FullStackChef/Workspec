CREATE TABLE [infrastructure].[InfrastructureBlueprintDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [infrastructure].[InfrastructureBlueprintDbRecord_History]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each infrastructure blueprint record.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'InfrastructureBlueprintDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the infrastructure blueprint, providing a clear identifier for internal reference.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'InfrastructureBlueprintDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the infrastructure blueprint and its purpose, outlining how it supports business operations.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'InfrastructureBlueprintDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO