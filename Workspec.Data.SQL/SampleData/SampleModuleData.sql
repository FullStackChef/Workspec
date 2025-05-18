-- Insert modules into ModuleDbRecord table
INSERT INTO [products].[ModuleDbRecord] 
    (TenantId, ProductId, ObjectId, Name, Description)
VALUES
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', 1, NEWID(), 'Infrastructure', 'Manages all aspects of the physical and software infrastructure necessary for the product operation.'),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', 1, NEWID(), 'Systems', 'Encapsulates all components and capabilities that provide essential functionalities to the product features.'),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', 1, NEWID(), 'Projects', 'Represents distinct bodies of work, focusing on the development and deployment of product features.'),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', 1, NEWID(), 'Products', 'Covers all features and modules offered as part of the product to the customers.'),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', 1, NEWID(), 'Personnel', 'Handles all user and stakeholder information, including roles, responsibilities, and interactions.'),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', 1, NEWID(), 'Reporting', 'Facilitates the creation and distribution of reports concerning various aspects of the software lifecycle and project management.');
GO
