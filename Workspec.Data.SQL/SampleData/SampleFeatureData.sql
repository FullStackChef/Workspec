-- Insert features into FeatureDbRecord table
INSERT INTO [products].[FeatureDbRecord] 
    (TenantId, ModuleId, ObjectId, Name, Description, Status, Priority, ReleaseVersion)
VALUES
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', 6, NEWID(), 'Risk Reporting', 'Facilitates the recording and tracking of potential risks associated with software development.', 'Planned', 1, '1.0'),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', 4, NEWID(), 'User Requirements', 'Allows users to define and manage software requirements and ensure they are met throughout the project.', 'Planned', 2, '1.0'),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', 1, NEWID(), 'Infrastructure Design', 'Enables the design and documentation of software infrastructure to support development.', 'Planned', 3, '1.0'),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', 4, NEWID(), 'Architectural Decisions', 'Tracks decisions made regarding the software architecture, providing a historical record and rationale.', 'Planned', 4, '1.0')
GO
