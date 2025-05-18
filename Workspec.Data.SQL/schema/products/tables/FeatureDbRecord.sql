-- Create FeatureDbRecord table with system versioning and constraints
CREATE TABLE [products].[FeatureDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [ModuleId] INT NOT NULL,
    [ObjectId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] TEXT,
    [Status] NVARCHAR(50),
    [Priority] INT,
    [ReleaseVersion] NVARCHAR(50),
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT [FK_FeatureDbRecord_ModuleId] FOREIGN KEY ([ModuleId]) REFERENCES [products].[ModuleDbRecord]([Id])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [products].[FeatureDbRecordHistory]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each feature record.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'FeatureDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the tenant under which this feature record is registered.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'FeatureDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the feature, clearly identifying the feature within the module.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'FeatureDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the feature, outlining its purpose and functionalities.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'FeatureDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The current status of the feature, e.g., Planned, In Development, Testing, Completed.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'FeatureDbRecord',
    @level2type = N'COLUMN', @level2name = 'Status';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The priority level of the feature, helping in prioritization and planning.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'FeatureDbRecord',
    @level2type = N'COLUMN', @level2name = 'Priority';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The version of the product release in which the feature is included.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'FeatureDbRecord',
    @level2type = N'COLUMN', @level2name = 'ReleaseVersion';
GO
