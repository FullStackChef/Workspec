-- Insert products into ProductDBRecord table
INSERT INTO [products].[ProductDBRecord] 
    (TenantId, ObjectId, Name,Code, Description, IsActive)
VALUES
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'Workspec', 'A comprehensive SaaS platform designed to manage the software development lifecycle, focusing on architecture, documentation, and traceability.', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'Clutch', 'A personal and household management app designed for efficient budgeting and financial planning.', 1),
    ('a56a6840-28ec-411a-a4bd-53ba95ffae65', NEWID(), 'Addressible Health Systems', 'A health systems management platform that ensures high interoperability and customization for healthcare providers.', 1);
GO
