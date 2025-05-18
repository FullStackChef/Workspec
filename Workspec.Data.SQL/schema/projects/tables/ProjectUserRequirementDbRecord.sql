CREATE TABLE [projects].[ProjectUserRequirementDbRecord]
(
	[ProjectId] INT NOT NULL, 
    [UserRequirementId] INT NOT NULL, 
    CONSTRAINT [PK_ProjectUserRequirementDbRecord] PRIMARY KEY ([ProjectId],[UserRequirementId]), 
    
    CONSTRAINT [FK_ProjectUserRequirementDbRecord_Project] 
        FOREIGN KEY ([ProjectId]) REFERENCES [projects].[ProjectDbRecord]([Id]),
    
    CONSTRAINT [FK_ProjectUserRequirementDbRecord_UserRequirement] 
        FOREIGN KEY ([UserRequirementId]) REFERENCES [projects].[UserRequirementDbRecord]([Id])
)
