
-- =============================================
-- BLOOD DONATION SYSTEM - SQL SERVER VERSION (COMPLETE)
-- =============================================

-- Lookup table for BloodGroup
CREATE TABLE BloodGroup (
    Id INT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO BloodGroup (Id, Name) VALUES
(1, 'o_plus'), (2, 'o_minus'),
(3, 'a_plus'), (4, 'a_minus'),
(5, 'b_plus'), (6, 'b_minus'),
(7, 'ab_plus'), (8, 'ab_minus');

-- Lookup table for RequestPriority
CREATE TABLE RequestPriority (
    Id INT PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL UNIQUE
);
INSERT INTO RequestPriority (Id, Name) VALUES
(1, 'low'), (2, 'medium'), (3, 'high');

-- Lookup table for AppointmentStatus
CREATE TABLE AppointmentStatus (
    Id INT PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL UNIQUE
);
INSERT INTO AppointmentStatus (Id, Name) VALUES
(1, 'on_process'), (2, 'approved'), (3, 'checked_in'),
(4, 'donated'), (5, 'done'), (6, 'rejected');

-- Lookup table for DonationType
CREATE TABLE DonationType (
    Id INT PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL UNIQUE
);
INSERT INTO DonationType (Id, Name) VALUES
(1, 'whole_blood'), (2, 'power_red'), (3, 'platelet'), (4, 'plasma');

-- Lookup table for BloodComponent
CREATE TABLE BloodComponent (
    Id INT PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL UNIQUE
);
INSERT INTO BloodComponent (Id, Name) VALUES
(1, 'red_cell'), (2, 'platelet'), (3, 'plasma');

-- Accounts table
CREATE TABLE Accounts (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Role NVARCHAR(20) NOT NULL,
    Email NVARCHAR(128) NOT NULL UNIQUE,
    Password NVARCHAR(72) NOT NULL,
    Phone NVARCHAR(16) UNIQUE,
    Name NVARCHAR(64),
    Gender NVARCHAR(10),
    Address NVARCHAR(MAX),
    Birthday DATE,
    BloodGroupId INT FOREIGN KEY REFERENCES BloodGroup(Id),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

-- BloodRequests
CREATE TABLE BloodRequests (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    StaffId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Accounts(Id),
    PriorityId INT NOT NULL FOREIGN KEY REFERENCES RequestPriority(Id),
    Title NVARCHAR(MAX) NOT NULL,
    MaxPeople INT NOT NULL,
    StartTime DATETIME2 NOT NULL,
    EndTime DATETIME2 NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

-- RequestBloodGroups
CREATE TABLE RequestBloodGroups (
    RequestId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES BloodRequests(Id),
    BloodGroupId INT NOT NULL FOREIGN KEY REFERENCES BloodGroup(Id),
    PRIMARY KEY (RequestId, BloodGroupId)
);

-- Appointments
CREATE TABLE Appointments (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    RequestId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES BloodRequests(Id),
    MemberId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Accounts(Id),
    StatusId INT NOT NULL FOREIGN KEY REFERENCES AppointmentStatus(Id),
    Reason NVARCHAR(MAX),
    CONSTRAINT UQ_Appointment UNIQUE (RequestId, MemberId)
);

-- Questions
CREATE TABLE Questions (
    Id INT PRIMARY KEY,
    Content NVARCHAR(MAX) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Answers
CREATE TABLE Answers (
    QuestionId INT NOT NULL FOREIGN KEY REFERENCES Questions(Id),
    AppointmentId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Appointments(Id),
    Content NVARCHAR(MAX) NOT NULL,
    PRIMARY KEY (QuestionId, AppointmentId)
);

-- Healths
CREATE TABLE Healths (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    AppointmentId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Appointments(Id),
    Temperature FLOAT NOT NULL,
    Weight FLOAT NOT NULL,
    UpperBloodPressure INT NOT NULL,
    LowerBloodPressure INT NOT NULL,
    HeartRate INT NOT NULL,
    IsGoodHealth BIT NOT NULL,
    Note NVARCHAR(MAX),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

-- Donations
CREATE TABLE Donations (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    AppointmentId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Appointments(Id),
    TypeId INT NOT NULL FOREIGN KEY REFERENCES DonationType(Id),
    Amount INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

-- BloodBags
CREATE TABLE BloodBags (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    DonationId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Donations(Id),
    ComponentId INT NOT NULL FOREIGN KEY REFERENCES BloodComponent(Id),
    IsUsed BIT NOT NULL DEFAULT 0,
    Amount INT NOT NULL,
    ExpiredTime DATETIME2 NOT NULL
);
