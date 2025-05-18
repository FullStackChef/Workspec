-- Create ProductDBRecord table with system versioning
CREATE TABLE [products].[ProductDBRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [ObjectId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Code] NVARCHAR(10) NOT NULL,
    [Description] TEXT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL, 
    CONSTRAINT [AK_ProductDBRecord_Code] UNIQUE ([Code]), 
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [products].[ProductDBRecordHistory]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The unique identifier for the product. Primary key.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ProductDBRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Globally unique identifier for the product, generated automatically.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ProductDBRecord',
    @level2type = N'COLUMN', @level2name = 'ObjectId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The identifier of the tenant under which this product is registered, supporting multi-tenancy.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ProductDBRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO


GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The name of the product.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ProductDBRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the product, can be NULL.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ProductDBRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Indicates whether the product is active. Default is 1 (active).',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ProductDBRecord',
    @level2type = N'COLUMN', @level2name = 'IsActive';
GO
