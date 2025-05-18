-- Insert records into ProjectDbRecord table with fictional software project details
INSERT INTO [projects].[ProjectDbRecord] 
    (TenantId, ObjectId, Name, Description, OwnerId, Status)
VALUES
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'InnovateSoft System Upgrade', 'Upgrading core system functionalities for better performance.', 'a8e1012f-1f31-49c8-84f6-e89b25fd0900', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'AlphaNet Security Enhancement', 'Enhancing network security to meet updated compliance standards.', 'a8e1012f-1f31-49c8-84f6-e89b25fd0900', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'BetaTech Data Integration', 'Integrating multiple data sources to streamline operations.', 'a8e1012f-1f31-49c8-84f6-e89b25fd0900', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'Gamma DevOps Setup', 'Setting up a complete DevOps pipeline for continuous deployment.', 'a8e1012f-1f31-49c8-84f6-e89b25fd0900', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'Delta Virtual Platform', 'Developing a virtual platform for remote collaboration.', 'a8e1012f-1f31-49c8-84f6-e89b25fd0900', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'Epsilon Mobile App', 'Creating a new mobile application to expand market reach.', 'a8e1012f-1f31-49c8-84f6-e89b25fd0900', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'Zeta Cloud Solutions', 'Designing cloud-based solutions to enhance data accessibility.', 'a8e1012f-1f31-49c8-84f6-e89b25fd0900', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'Eta Software Modernization', 'Modernizing legacy software systems to current tech standards.', 'a8e1012f-1f31-49c8-84f6-e89b25fd0900', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'Theta Analytics Engine', 'Building an advanced analytics engine to provide deeper insights into data.', 'a8e1012f-1f31-49c8-84f6-e89b25fd0900', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'Iota Payment Gateway', 'Developing a secure payment gateway to facilitate online transactions.', 'a8e1012f-1f31-49c8-84f6-e89b25fd0900', 1);
