CREATE TABLE [products].[ProductStakeholderDbRecord] (
    [ProductId] INT NOT NULL,
    [StakeholderPortfolioId] INT NOT NULL,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT [FK_ProductStakeholderDbRecord_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [products].[ProductDBRecord]([Id]),
    CONSTRAINT [FK_ProductStakeholderDbRecord_StakeholderPortfolioId] FOREIGN KEY ([StakeholderPortfolioId]) REFERENCES [personnel].[StakeholderPortfolioDbRecord]([Id]), 
    CONSTRAINT [PK_ProductStakeholderDbRecord] PRIMARY KEY ([ProductId],[StakeholderPortfolioId]),
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [products].[ProductStakeholderDbRecordHistory]));

GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the product associated with a stakeholder portfolio.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ProductStakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'ProductId';
GO;

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the stakeholder portfolio associated with this product.',
    @level0type = N'SCHEMA', @level0name = 'products', 
    @level1type = N'TABLE',  @level1name = 'ProductStakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'StakeholderPortfolioId';
GO;
