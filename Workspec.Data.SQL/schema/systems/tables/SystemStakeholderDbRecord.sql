CREATE TABLE [systems].[SystemStakeholderDbRecord] (
    [SystemId] INT NOT NULL,
    [StakeholderPortfolioId] INT NOT NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT [FK_SystemStakeholderDbRecord_SystemId] FOREIGN KEY ([SystemId]) REFERENCES [systems].[SystemDBRecord]([Id]),
    CONSTRAINT [FK_SystemStakeholderDbRecord_StakeholderPortfolioId] FOREIGN KEY ([StakeholderPortfolioId]) REFERENCES [personnel].[StakeholderPortfolioDbRecord]([Id]), 
    CONSTRAINT [PK_SystemStakeholderDbRecord] PRIMARY KEY ([SystemId],[StakeholderPortfolioId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [systems].[SystemStakeholderDbRecordHistory]));

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the product associated with a stakeholder portfolio.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'SystemStakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'SystemId';
GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the stakeholder portfolio associated with this product.',
    @level0type = N'SCHEMA', @level0name = 'systems', 
    @level1type = N'TABLE',  @level1name = 'SystemStakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'StakeholderPortfolioId';
GO;
