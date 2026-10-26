CREATE TABLE dbo.CharterFixtures (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Reference NVARCHAR(40) NOT NULL UNIQUE,
    VesselName NVARCHAR(120) NOT NULL,
    Charterer NVARCHAR(160) NOT NULL,
    LoadPort NVARCHAR(120) NOT NULL,
    DischargePort NVARCHAR(120) NOT NULL,
    FreightRate DECIMAL(18,2) NOT NULL CHECK (FreightRate > 0),
    Currency CHAR(3) NOT NULL DEFAULT 'USD',
    Status NVARCHAR(30) NOT NULL DEFAULT 'Draft',
    DecisionNote NVARCHAR(1000) NULL,
    CreatedAtUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    DecidedAtUtc DATETIME2 NULL
);
GO
CREATE INDEX IX_CharterFixtures_Status_CreatedAt ON dbo.CharterFixtures(Status, CreatedAtUtc DESC);
GO
CREATE OR ALTER PROCEDURE dbo.GetFixturesByStatus @Status NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Reference, VesselName, Charterer, LoadPort, DischargePort, FreightRate, Currency, Status, CreatedAtUtc
    FROM dbo.CharterFixtures WHERE Status = @Status ORDER BY CreatedAtUtc DESC;
END;
GO
