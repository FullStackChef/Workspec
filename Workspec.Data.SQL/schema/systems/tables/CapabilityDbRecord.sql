-- Create CapabilityDbRecord table with system versioning and constraints
CREATE TABLE [products].[CapabilityDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [ModuleId] INT NOT NULL,
    [ObjectId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] TEXT,
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT [FK_CapabilityDbRecord_ModuleId] FOREIGN KEY ([ModuleId]) REFERENCES [products].[ModuleDbRecord]([Id])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [products].[CapabilityDbRecordHistory]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each capability record.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'CapabilityDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the module to which this capability belongs, linking capabilities to their respective modules.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'CapabilityDbRecord',
    @level2type = N'COLUMN', @level2name = 'ModuleId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Globally unique identifier for the capability, automatically generated to ensure uniqueness across distributed systems.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'CapabilityDbRecord',
    @level2type = N'COLUMN', @level2name = 'ObjectId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the capability, providing a clear and concise identifier that is easily recognizable within the module.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'CapabilityDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the capability, outlining its purpose and functionalities within the module.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'CapabilityDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the tenant under which this capability record is registered, supporting multi-tenancy.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'CapabilityDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO
