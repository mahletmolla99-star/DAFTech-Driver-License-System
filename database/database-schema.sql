-- DAFTech Driver License System Database Schema
-- Created: 2026-07-17

CREATE TABLE [Users] (
    [UserId] INT PRIMARY KEY IDENTITY(1,1),
    [Username] NVARCHAR(100) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(MAX) NOT NULL,
    [Status] NVARCHAR(50) DEFAULT 'Active',
    [CreatedAt] DATETIME DEFAULT GETDATE()
);

CREATE TABLE [Drivers] (
    [DriverId] INT PRIMARY KEY IDENTITY(1,1),
    [LicenseId] NVARCHAR(50) NOT NULL UNIQUE,
    [FullName] NVARCHAR(200) NOT NULL,
    [DateOfBirth] DATETIME,
    [LicenseType] NVARCHAR(50),
    [ExpiryDate] DATETIME,
    [QRRawData] NVARCHAR(MAX),
    [OCRRawText] NVARCHAR(MAX),
    [Status] NVARCHAR(50) DEFAULT 'Active',
    [CreatedAt] DATETIME DEFAULT GETDATE(),
    [RegisteredBy] INT,
    FOREIGN KEY ([RegisteredBy]) REFERENCES [Users]([UserId])
);

CREATE TABLE [VerificationLogs] (
    [LogId] INT PRIMARY KEY IDENTITY(1,1),
    [LicenseId] NVARCHAR(50) NOT NULL,
    [VerificationStatus] NVARCHAR(50),
    [CheckedBy] INT,
    [CheckedDate] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([CheckedBy]) REFERENCES [Users]([UserId]),
    FOREIGN KEY ([LicenseId]) REFERENCES [Drivers]([LicenseId])
);

INSERT INTO [Users] ([Username], [PasswordHash], [Status])
VALUES ('testuser', 'hashed_password_here', 'Active');
