-- Create SystemDbRecord table with system versioning and constraints
CREATE TABLE [systems].[SystemDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [ObjectId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] TEXT,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [systems].[SystemDbRecordHistory]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each system record.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'SystemDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the tenant under which this system record is registered, supporting multi-tenancy.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'SystemDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Globally unique identifier for the system, automatically generated to ensure uniqueness across distributed systems and external integrations.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'SystemDbRecord',
    @level2type = N'COLUMN', @level2name = 'ObjectId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the system, clearly identifying the system within the organization.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'SystemDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the system, outlining its purpose and key functionalities.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'SystemDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO
