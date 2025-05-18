CREATE TABLE [personnel].[StakeholderPortfolioDbRecord] (
    [Id] INT  IDENTITY(1,1),
    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [StakeholderId] BIGINT NOT NULL,
    [StakeholderTypeId] INT NOT NULL,
    [Description] TEXT,
    [SysStartTime] DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    [SysEndTime] DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime]),
    CONSTRAINT FK_StakeholderPortfolioDbRecord_StakeholderTypeId FOREIGN KEY ([StakeholderTypeId]) REFERENCES [personnel].[StakeholderTypeDbRecord] ([Id]),
    CONSTRAINT FK_StakeholderPortfolioDbRecord_StakeholderId FOREIGN KEY ([StakeholderId]) REFERENCES [personnel].[StakeholderDbRecord] ([Id]), 
    CONSTRAINT [PK_StakeholderPortfolioDbRecord] PRIMARY KEY ([Id]) 
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [personnel].[StakeholderPortfolioDbRecordHistory]));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each stakeholder portfolio record.',
    @level0type = N'SCHEMA', @level0name = 'personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderPortfolioDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the tenant under which this portfolio record is registered, supporting multi-tenancy.',
    @level0type = N'SCHEMA', @level0name = 'personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderPortfolioDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the stakeholder to whom this portfolio is assigned.',
    @level0type = N'SCHEMA', @level0name = 'personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderPortfolioDbRecord',
    @level2type = N'COLUMN', @level2name = 'StakeholderId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Link to the type of stakeholder this portfolio is associated with, such as Product Owner, Developer, etc.',
    @level0type = N'SCHEMA', @level0name = 'personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderPortfolioDbRecord',
    @level2type = N'COLUMN', @level2name = 'StakeholderTypeId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the portfolio, outlining the scope and responsibilities associated with it.',
    @level0type = N'SCHEMA', @level0name = 'personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderPortfolioDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO
