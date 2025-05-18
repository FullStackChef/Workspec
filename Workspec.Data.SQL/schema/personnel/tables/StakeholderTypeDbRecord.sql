-- Create StakeholderTypeDbRecord table with system versioning
CREATE TABLE Personnel.StakeholderTypeDbRecord (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Description TEXT NULL,
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = Personnel.StakeholderTypeDbRecordHistory));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each stakeholder type.',
    @level0type = N'SCHEMA', @level0name = 'Personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderTypeDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the tenant under which this stakeholder type is registered, supporting multi-tenancy.',
    @level0type = N'SCHEMA', @level0name = 'Personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderTypeDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Name of the stakeholder type, such as Developer, Project Manager, QA Analyst, etc.',
    @level0type = N'SCHEMA', @level0name = 'Personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderTypeDbRecord',
    @level2type = N'COLUMN', @level2name = 'Name';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'A detailed description of the stakeholder type, outlining responsibilities and the role within projects.',
    @level0type = N'SCHEMA', @level0name = 'Personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderTypeDbRecord',
    @level2type = N'COLUMN', @level2name = 'Description';
GO
