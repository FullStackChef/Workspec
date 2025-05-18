-- Create ResourceDbRecord table with integer status
CREATE TABLE [infrastructure].[ResourceDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [ResourceTypeId] INT NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX),
    [Status] INT,  -- Status now as integer
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [infrastructure].[ResourceDbRecord_History]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each resource.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'ResourceDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the type of resource, linking to the ResourceType table.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'ResourceDbRecord',
    @level2type = N'COLUMN', @level2name = 'ResourceTypeId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the resource.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'ResourceDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Description of the resource, providing details about its purpose and use.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'ResourceDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Current status of the resource, indicated by an integer code representing operational states.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'ResourceDbRecord',
    @level2type = N'COLUMN', @level2name = 'Status';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the tenant under which this resource is registered.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'ResourceDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO
