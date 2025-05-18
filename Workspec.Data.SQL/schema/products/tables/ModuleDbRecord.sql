-- Create [ModuleDbRecord] table with system versioning and constraints
CREATE TABLE [products].[ModuleDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [ProductId] INT NOT NULL,
    [ObjectId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] TEXT,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT [FK_ModuleDbRecord_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [products].[ProductDbRecord]([Id])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [products].[ModuleDbRecordHistory]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each module record.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ModuleDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the tenant under which this module record is registered. Supports multi-tenancy without foreign key.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ModuleDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Globally unique identifier for the module, automatically generated.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ModuleDbRecord',
    @level2type = N'COLUMN', @level2name = 'ObjectId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the module, clearly identifying the module within the product.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ModuleDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the module, outlining its purpose and key functionalities.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ModuleDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the product associated with this module.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ModuleDbRecord',
    @level2type = N'COLUMN', @level2name = 'ProductId';
GO
