CREATE DATABASE DrugContraindicationDB;
GO

USE DrugContraindicationDB;

CREATE TABLE Users(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    PasswordHash NVARCHAR(255),
    Role NVARCHAR(20)
);

CREATE TABLE Drugs(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DrugName NVARCHAR(100),
    ActiveIngredient NVARCHAR(100)
);

CREATE TABLE Diseases(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DiseaseName NVARCHAR(100)
);

CREATE TABLE Contraindications(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DrugId INT,
    DiseaseId INT,
    WarningMessage NVARCHAR(500),

    FOREIGN KEY (DrugId) REFERENCES Drugs(Id),
    FOREIGN KEY (DiseaseId) REFERENCES Diseases(Id)
);

CREATE TABLE Histories(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    SearchDate DATETIME,
    Result NVARCHAR(MAX),

    FOREIGN KEY(UserId) REFERENCES Users(Id)
);