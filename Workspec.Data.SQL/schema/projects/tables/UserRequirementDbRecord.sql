-- Format document
CREATE TABLE [projects].[UserRequirementDbRecord]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [ObjectId] UNIQUEIDENTIFIER NOT NULL,
    [FeatureId] INT NOT NULL,
    [Title] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [Priority] INT NOT NULL,
    [StatusId] INT NOT NULL,
    [Version] NVARCHAR(20) NOT NULL,
    [Tags] NVARCHAR(MAX) NOT NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT [FK_UserRequirementDbRecord_FeatureDbRecord] FOREIGN KEY ([FeatureId]) REFERENCES [products].[FeatureDbRecord]([Id])
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [projects].[UserRequirementDbRecord_History]));

GO;

-- Add extended properties for better documentation
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The unique identifier of the user requirement.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'Id';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The unique identifier of the tenant.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'TenantId';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The unique identifier of the object.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'ObjectId';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The foreign key reference to the feature.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'FeatureId';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The title of the user requirement.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'Title';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The description of the user requirement.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'Description';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The priority of the user requirement.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'Priority';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The status ID of the user requirement.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'StatusId';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The version of the user requirement.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'Version';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The tags associated with the user requirement.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'Tags';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The start time of the system versioning period.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'SysStartTime';

GO;

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The end time of the system versioning period.', @level0type=N'SCHEMA', @level0name=N'projects', @level1type=N'TABLE', @level1name=N'UserRequirementDbRecord', @level2type=N'COLUMN', @level2name=N'SysEndTime';

GO;
