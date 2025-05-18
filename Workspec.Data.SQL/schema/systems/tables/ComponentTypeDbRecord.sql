CREATE TABLE [systems].[ComponentTypeDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX)
);

GO;

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for the component type.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentTypeDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the component type, used for display and selection.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentTypeDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
    
GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Optional description of the component type, providing more details about its purpose or usage.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'ComponentTypeDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
    
GO;