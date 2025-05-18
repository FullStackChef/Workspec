-- Declare variables to store project IDs and a table to hold them
DECLARE @ProjectIds TABLE (Id INT);
INSERT INTO @ProjectIds (Id)
SELECT TOP 10 Id FROM [projects].[ProjectDbRecord];

-- Variables to hold risk information
DECLARE @TenantId UNIQUEIDENTIFIER = 'a56a6840-28ec-411a-a4bd-53ba95ffae65';
DECLARE @OwnerId UNIQUEIDENTIFIER = 'a8e1012f-1f31-49c8-84f6-e89b25fd0900';
DECLARE @ProjectId INT;
DECLARE @i INT = 1;

-- Loop to insert 300 risks randomly distributed across the 10 projects
WHILE @i <= 300
BEGIN
    -- Randomly select a project ID from the table
    SELECT TOP 1 @ProjectId = Id FROM @ProjectIds ORDER BY NEWID();

    -- Insert a risk into the RiskDbRecord table
    INSERT INTO [projects].[RiskDbRecord]
    (
        TenantId,
        OwnerId,
        ProjectId,
        RiskClassId,
        Description,
        ImpactDescription,
        ImpactLevel,
        Probability,
        MitigationStrategy,
        ActionId,
        Reassess
    )
    VALUES
    (
        @TenantId,
        @OwnerId,
        @ProjectId,
        CEILING(RAND() * 7), -- Random RiskClassId between 1 and 7
        'Risk description ' + CAST(@i AS NVARCHAR(10)),
        'Impact description ' + CAST(@i AS NVARCHAR(10)),
        CEILING(RAND() * 5), -- Random ImpactLevel between 1 and 3
        CEILING(RAND() * 5), -- Random Probability between 1 and 5
        'Mitigation strategy ' + CAST(@i AS NVARCHAR(10)),
        CEILING(RAND() * 5), -- Random ActionId between 1 and 5
        DATEADD(DAY, CEILING(RAND() * 365), GETDATE()) -- Random Reassess date within the next year
    );

    -- Increment the counter
    SET @i = @i + 1;
END;
