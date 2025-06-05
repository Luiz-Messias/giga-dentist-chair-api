-- Cria o banco de dados apenas se não existir
CREATE DATABASE IF NOT EXISTS gigadb;
USE gigadb;

-- Cria a tabela DentistChairs se não existir
CREATE TABLE IF NOT EXISTS DentistChairs (
    Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    Number INT NOT NULL,
    Description VARCHAR(255) NOT NULL,
    IsActive BOOLEAN NOT NULL
);

-- Cria a tabela Allocations se não existir
CREATE TABLE IF NOT EXISTS Allocations (
    Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    ChairId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    FOREIGN KEY (ChairId) REFERENCES DentistChairs(Id)
);

-- Índices para melhorar performance das consultas
CREATE INDEX idx_allocations_chairid ON Allocations(ChairId);
CREATE INDEX idx_allocations_starttime ON Allocations(StartTime);
CREATE INDEX idx_allocations_endtime ON Allocations(EndTime);
CREATE INDEX idx_chairs_isactive ON DentistChairs(IsActive);
