-- Create the ProjectDbRecord table in the projects schema with full column definitions
CREATE TABLE [projects].[ProjectDbRecord]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [ObjectId] UNIQUEIDENTIFIER NOT NULL,
    [ProductId] INT NOT NULL ,
    [Name] NVARCHAR(255) NOT NULL,
    [Path] NVARCHAR(255) NULL ,
    [Description] NVARCHAR(MAX) NULL,
    [Status] INT NOT NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN,
    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
    CONSTRAINT [FK_ProjectDbRecord_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [products].[ProductDbRecord]([Id]), 
    CONSTRAINT [AK_ProjectDbRecord_Path] UNIQUE ([Path])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [projects].[ProjectDbRecordHistory]));

GO;

-- Adding descriptions to columns using extended properties
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The unique identifier for a project. Primary key.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'ProjectDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The identifier of the tenant under which this project is registered, supporting multi-tenancy.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'ProjectDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A globally unique identifier for the project, ensuring uniqueness across distributed systems and external integrations.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'ProjectDbRecord',
    @level2type = N'COLUMN', @level2name = 'ObjectId';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The name of the project, providing a brief but descriptive identification for the project.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'ProjectDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the project''s goals and scope, offering insights into the project’s objectives and strategic alignment.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'ProjectDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'An integer representation of the current status of the project, denoting its phase or condition in the project lifecycle.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'ProjectDbRecord',
    @level2type = N'COLUMN', @level2name = 'Status';
