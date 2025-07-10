-- Admin user
INSERT INTO Accounts (Id, Role, Email, Password, Phone, Name, Gender, Address, Birthday, BloodGroupId, IsActive, CreatedAt)
VALUES (
    NEWID(), 
    'admin', 
    'admin@test.com', 
    'admin123', 
    '+841234567890', 
    'Nguyen Van A', 
    'male', 
    '123 Le Loi, HCMC', 
    '1980-05-10', 
    1, -- o_plus
    1, 
    GETDATE()
);

-- Staff user
INSERT INTO Accounts (Id, Role, Email, Password, Phone, Name, Gender, Address, Birthday, BloodGroupId, IsActive, CreatedAt)
VALUES (
    NEWID(), 
    'staff', 
    'staff@test.com', 
    'staff123', 
    '+849876543210', 
    'Tran Thi B', 
    'female', 
    '456 Nguyen Hue, HCMC', 
    '1990-12-20', 
    4, -- a_minus
    1, 
    GETDATE()
);

-- Member user
INSERT INTO Accounts (Id, Role, Email, Password, Phone, Name, Gender, Address, Birthday, BloodGroupId, IsActive, CreatedAt)
VALUES (
    NEWID(), 
    'member', 
    'member@test.com', 
    'member123', 
    '+848765432109', 
    'Le Van C', 
    'male', 
    '789 Pasteur, HCMC', 
    '1995-07-15', 
    5, -- b_plus
    1, 
    GETDATE()
);
