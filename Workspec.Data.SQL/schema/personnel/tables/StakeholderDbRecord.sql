-- Create StakeholderDbRecord table with system versioning
CREATE TABLE Personnel.StakeholderDbRecord (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
    SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = Personnel.StakeholderDbRecordHistory));
GO

-- Add extended properties for better documentation
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Unique identifier for each stakeholder.',
    @level0type = N'SCHEMA', @level0name = 'Personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'Id';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Identifier for the tenant under which this stakeholder is registered, supporting multi-tenancy.',
    @level0type = N'SCHEMA', @level0name = 'Personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'TenantId';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'First name of the stakeholder.',
    @level0type = N'SCHEMA', @level0name = 'Personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'FirstName';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Last name of the stakeholder.',
    @level0type = N'SCHEMA', @level0name = 'Personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'LastName';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Email address of the stakeholder.',
    @level0type = N'SCHEMA', @level0name = 'Personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'Email';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = 'Indicates whether the stakeholder is currently active. Defaults to 1 (true).',
    @level0type = N'SCHEMA', @level0name = 'Personnel', 
    @level1type = N'TABLE',  @level1name = 'StakeholderDbRecord',
    @level2type = N'COLUMN', @level2name = 'IsActive';
GO
