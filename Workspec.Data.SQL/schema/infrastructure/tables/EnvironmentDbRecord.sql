-- Create EnvironmentDbRecord table with system versioning
CREATE TABLE [infrastructure].[EnvironmentDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [EnvironmentName] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [infrastructure].[EnvironmentDbRecord_History]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each environment record.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'EnvironmentDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the environment, identifying it within the organization (e.g., Development, Testing, Production).',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'EnvironmentDbRecord',
    @level2type = N'COLUMN', @level2name = 'EnvironmentName';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the environment and its purpose, outlining specific configurations and settings.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'EnvironmentDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the tenant under which this environment is registered, supporting multi-tenancy.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'EnvironmentDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO
