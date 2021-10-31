USE PerfJournal
GO

SET XACT_ABORT ON;
BEGIN TRAN

DROP TABLE IF EXISTS dbo.TestResult
DROP TABLE IF EXISTS dbo.Tester
DROP TABLE IF EXISTS dbo.Test
DROP TABLE IF EXISTS dbo.Environment
DROP TABLE IF EXISTS dbo.Build
DROP TABLE IF EXISTS dbo.Project

CREATE TABLE dbo.Project
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	ProjectName NVARCHAR(50) NOT NULL
)

CREATE TABLE dbo.Build
(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ProjectId INT FOREIGN KEY REFERENCES Project(Id) NOT NULL,
	Major INT NOT NULL,
	Minor INT NOT NULL,
	Patch INT NOT NULL
)

CREATE TABLE dbo.Environment
(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	EnvironmentName NVARCHAR(50) NOT NULL
)

CREATE TABLE dbo.Test
(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ProjectId INT FOREIGN KEY REFERENCES Project(Id) NOT NULL,
	TestName NVARCHAR(100) NOT NULL,
	TestDescription NVARCHAR(1000) NULL -- this is optional
)

ALTER TABLE Test   
ADD CONSTRAINT UX_ProjectTest UNIQUE (ProjectId, TestName);   

CREATE TABLE dbo.Tester
(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	TesterName NVARCHAR(100) NOT NULL 
)

CREATE TABLE dbo.TestResult
(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	TestId INT FOREIGN KEY REFERENCES Test(Id) NOT NULL,
	ProjectId INT FOREIGN KEY REFERENCES Project(Id) NOT NULL,
	BuildId INT FOREIGN KEY REFERENCES Build(Id) NULL, -- this is optional 
	EnvironmentId INT FOREIGN KEY REFERENCES Environment(Id) NULL, -- this is optional
	TesterId INT FOREIGN KEY REFERENCES Tester(Id) NULL, -- this is optional
	TestDate DATETIME2 NOT NULL,
	TotalTimeInMilliseconds INT NOT NULL,
	Notes NVARCHAR(1000) NULL, -- this is optional
	IsSuccessful BIT NOT NULL -- pass/fail
)

COMMIT TRAN
