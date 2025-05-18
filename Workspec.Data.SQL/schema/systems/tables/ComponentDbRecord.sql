-- Create ComponentDbRecord table with system versioning and constraints
CREATE TABLE [systems].[ComponentDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [SystemId] INT NOT NULL,
    [ComponentTypeId] INT NOT NULL,
    [ObjectId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT [FK_ComponentDbRecord_SystemId] FOREIGN KEY ([SystemId]) REFERENCES [systems].[SystemDbRecord]([Id]),
    CONSTRAINT [FK_ComponentDbRecord_ComponentTypeId] FOREIGN KEY ([ComponentTypeId]) REFERENCES [systems].[ComponentTypeDbRecord]([Id])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [systems].[ComponentDbRecordHistory]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each component record.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the system to which this component belongs, linking components to their respective systems.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentDbRecord',
    @level2type = N'COLUMN', @level2name = 'SystemId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the component type, categorizing this component under a specific type defined in ComponentTypeDbRecord.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentDbRecord',
    @level2type = N'COLUMN', @level2name = 'ComponentTypeId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Globally unique identifier for the component, automatically generated to ensure uniqueness across distributed systems.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentDbRecord',
    @level2type = N'COLUMN', @level2name = 'ObjectId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the component, providing a clear and concise identifier that is easily recognizable.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the component, outlining its purpose and functionalities within the system.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the tenant under which this component record is registered, supporting multi-tenancy.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO
