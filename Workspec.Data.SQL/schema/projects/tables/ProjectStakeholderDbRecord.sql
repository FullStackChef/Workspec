CREATE TABLE [projects].[ProjectStakeholderDbRecord] (
    [ProjectId] INT NOT NULL,
    [StakeholderPortfolioId] INT NOT NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT [FK_ProjectStakeholderDbRecord_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [projects].[ProjectDBRecord]([Id]),
    CONSTRAINT [FK_ProjectStakeholderDbRecord_StakeholderPortfolioId] FOREIGN KEY ([StakeholderPortfolioId]) REFERENCES [personnel].[StakeholderPortfolioDbRecord]([Id]), 
    CONSTRAINT [PK_ProjectStakeholderDbRecord] PRIMARY KEY ([ProjectId],[StakeholderPortfolioId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [projects].[ProjectStakeholderDbRecordHistory]));

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the product associated with a stakeholder portfolio.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'ProjectStakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'ProjectId';
GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the stakeholder portfolio associated with this product.',
    @level0type = N'SCHEMA', @level0name = 'projects', 
    @level1type = N'TABLE',  @level1name = 'ProjectStakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'StakeholderPortfolioId';
GO;
