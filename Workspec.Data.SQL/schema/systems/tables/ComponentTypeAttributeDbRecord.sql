-- Create ComponentTypeAttributeDbRecord table with system versioning and constraints
CREATE TABLE [systems].[ComponentTypeAttributeDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [ComponentTypeId] INT NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX),
    [DefaultValue] NVARCHAR(255),
    [IsRequired] BIT NOT NULL DEFAULT 0,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT [FK_ComponentTypeAttributeDbRecord_ComponentTypeId] FOREIGN KEY ([ComponentTypeId]) REFERENCES [systems].[ComponentTypeDbRecord]([Id])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [systems].[ComponentTypeAttributeDbRecordHistory]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each component type attribute record.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentTypeAttributeDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the component type to which this attribute belongs, linking attributes to their respective component types.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentTypeAttributeDbRecord',
    @level2type = N'COLUMN', @level2name = 'ComponentTypeId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the attribute, used for display and identification within the application.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentTypeAttributeDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Optional description of the attribute, providing more details about its purpose or usage.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentTypeAttributeDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Default value for the attribute, used when no specific value is provided during configuration.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentTypeAttributeDbRecord',
    @level2type = N'COLUMN', @level2name = 'DefaultValue';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Indicates whether the attribute is required to be filled for the component type. Defaults to false (0).',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentTypeAttributeDbRecord',
    @level2type = N'COLUMN', @level2name = 'IsRequired';
GO
