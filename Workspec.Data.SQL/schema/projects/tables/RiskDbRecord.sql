CREATE TABLE [projects].[RiskDbRecord]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [OwnerId] UNIQUEIDENTIFIER NOT NULL,
    [ProjectId] INT NOT NULL,
    [RiskClassId] INT NOT NULL,
    [Description] NVARCHAR(255) NOT NULL,
    [ImpactDescription] NVARCHAR(255) NOT NULL,
    [ImpactLevel] INT NOT NULL,
    [Probability] INT NOT NULL,
    [MitigationStrategy] NVARCHAR(255) NOT NULL,
    [ActionId] INT NOT NULL,
    [Reassess] DATETIME NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN,
    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime),
    CONSTRAINT [FK_RiskDbRecords_ProjectId] FOREIGN KEY (ProjectId) REFERENCES [projects].[ProjectDbRecord](Id) ON DELETE CASCADE ON UPDATE CASCADE
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [projects].[RiskDbRecordHistory]));

GO;

-- Add descriptions for RiskDbRecord table columns
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each risk record. Primary key.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
    
GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier linking the risk to a specific tenant.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'The identifier of the user who owns or manages the risk.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'OwnerId';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier of the project this risk is associated with. Foreign key linking to ProjectDbRecords.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'ProjectId';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Classifies the risk by type, linked to a Risk Classification table.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'RiskClassId';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Brief description of the risk.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Description of the risk''s potential impact on the project.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'ImpactDescription';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Numerical level of impact, quantifying the severity of the risk.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'ImpactLevel';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Likelihood of the risk occurring, expressed as a percentage.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'Probability';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Describes the strategy to mitigate the risk.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'MitigationStrategy';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Links to an action table detailing specific mitigation actions taken for this risk.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'ActionId';

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Optional date for when the risk should be reassessed to evaluate new circumstances or the effectiveness of mitigation strategies.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'RiskDbRecord',
    @level2type = N'COLUMN', @level2name = 'Reassess';

GO;