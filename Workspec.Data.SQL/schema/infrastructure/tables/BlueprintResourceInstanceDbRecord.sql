-- Create BlueprintResourceInstanceDbRecord table with system versioning and named foreign key constraints
CREATE TABLE [infrastructure].[BlueprintResourceInstanceDbRecord] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [BlueprintId] INT NOT NULL,
    [ResourceId] INT NOT NULL,
    [ProviderId] INT NOT NULL,  -- Linking to the ProviderDbRecord
    [InstanceName] NVARCHAR(255) NOT NULL,  -- Name as it would appear in Terraform or other IaC tools
    [Description] NVARCHAR(MAX),
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT [FK_BlueprintResourceInstance_Blueprint] FOREIGN KEY ([BlueprintId]) REFERENCES [infrastructure].[InfrastructureBlueprintDbRecord]([Id]),
    CONSTRAINT [FK_BlueprintResourceInstance_Resource] FOREIGN KEY ([ResourceId]) REFERENCES [infrastructure].[ResourceDbRecord]([Id]),
    CONSTRAINT [FK_BlueprintResourceInstance_Provider] FOREIGN KEY ([ProviderId]) REFERENCES [infrastructure].[ProviderDbRecord]([Id])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [infrastructure].[BlueprintResourceInstanceDbRecord_History]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each instance of a resource within a blueprint.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'BlueprintResourceInstanceDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the blueprint that this resource instance is part of.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'BlueprintResourceInstanceDbRecord',
    @level2type = N'COLUMN', @level2name = 'BlueprintId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the resource that this instance represents.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'BlueprintResourceInstanceDbRecord',
    @level2type = N'COLUMN', @level2name = 'ResourceId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the provider that manages this resource instance.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'BlueprintResourceInstanceDbRecord',
    @level2type = N'COLUMN', @level2name = 'ProviderId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the resource instance, as it would be used in Terraform or similar IaC configurations.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'BlueprintResourceInstanceDbRecord',
    @level2type = N'COLUMN', @level2name = 'InstanceName';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the resource instance, explaining its role within the blueprint.',
    @level0type = N'SCHEMA', @level0name = 'infrastructure', 
    @level1type = N'TABLE',  @level1name = 'BlueprintResourceInstanceDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO
