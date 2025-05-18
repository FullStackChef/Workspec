SET IDENTITY_INSERT [projects].[UserRequirementDbRecord] ON;

-- Insert user requirements for Risk Reporting into UserRequirementDbRecord table
INSERT INTO [projects].[UserRequirementDbRecord]
    (Id, TenantId, ObjectId, FeatureId, Title, Description, Priority, StatusId, Version, Tags)
VALUES
    (101, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 1, 'Risk Data Importation', 'Enable importing risk data from various external sources to centralize risk management.', 1, 1, '1.0', 'import, external'),
    (102, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 1, 'Risk Categorization', 'Systematically categorize risks based on type and impact to improve response strategies.', 2, 1, '1.0', 'categorization, type, impact'),
    (103, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 1, 'Risk Prioritization', 'Automatically prioritize risks based on their impact and probability factors.', 3, 1, '1.0', 'prioritization, impact, probability'),
    (104, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 1, 'Custom Risk Metrics', 'Allow users to define and use custom metrics for risk evaluation.', 4, 1, '1.0', 'custom, metrics'),
    (105, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 1, 'Risk Impact Analysis', 'Provide detailed analysis tools to assess the potential impacts of identified risks.', 5, 1, '1.0', 'impact, analysis'),
    (106, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 1, 'Interactive Risk Dashboards', 'Develop interactive dashboards to display real-time data on risk status and progression.', 6, 1, '1.0', 'dashboards, interactive'),
    (107, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 1, 'Risk Mitigation Tracking', 'Track the implementation and status of risk mitigation strategies.', 7, 1, '1.0', 'mitigation, tracking'),
    (108, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 1, 'Automated Risk Alerts', 'Automate alerts for risks that exceed predefined thresholds.', 8, 1, '1.0', 'alerts, automated'),
    (109, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 1, 'Risk Report Generation', 'Enable the generation of detailed and customizable risk reports.', 9, 1, '1.0', 'reports, generation'),
    (110, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 1, 'Historical Risk Data Analysis', 'Analyze historical risk data to identify patterns and predict future risk scenarios.', 10, 1, '1.0', 'historical, prediction'),
    (201, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 2, 'Requirement Capture', 'Enable easy capture and logging of new user requirements.', 1, 1, '1.0', 'capture, logging'),
    (202, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 2, 'Requirement Editing', 'Facilitate editing and updating existing user requirements.', 2, 1, '1.0', 'editing, updating'),
    (203, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 2, 'Requirement Versioning', 'Maintain a history of edits with comprehensive version tracking for each requirement.', 3, 1, '1.0', 'versioning, history'),
    (204, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 2, 'Requirement Categorization', 'Classify requirements into categories or types for structured management.', 4, 1, '1.0', 'categorization, types'),
    (205, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 2, 'Requirement Prioritization', 'Prioritize requirements based on business impact and project timelines.', 5, 1, '1.0', 'prioritization, impact'),
    (206, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 2, 'Requirement Status Tracking', 'Track the status of each requirement through its lifecycle.', 6, 1, '1.0', 'tracking, status'),
    (207, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 2, 'Requirement Approval Process', 'Implement an approval workflow for validating and approving requirements.', 7, 1, '1.0', 'approval, workflow'),
    (208, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 2, 'Requirement Dependency Mapping', 'Map and visualize dependencies between requirements.', 8, 1, '1.0', 'mapping, dependencies'),
    (209, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 2, 'Reporting and Analytics', 'Generate detailed reports and analytics on the status and progress of requirements.', 9, 1, '1.0', 'reporting, analytics'),
    (210, 'a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 2, 'Notification System', 'Set up notifications for requirement updates, changes, or upcoming deadlines.', 10, 1, '1.0', 'notifications, alerts');
GO

SET IDENTITY_INSERT [projects].[UserRequirementDbRecord] OFF;

