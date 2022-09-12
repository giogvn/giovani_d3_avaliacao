CREATE DATABASE [giovani_d3_avaliacao];
GO

USE [giovani_d3_avaliacao];
GO

CREATE TABLE Users(
    id      VARCHAR(255)    PRIMARY KEY,
    nome    VARCHAR(255)    NOT NULL,
    email   VARCHAR(255)    NOT NULL,
    senha   VARCHAR(255)    NOT NULL
);
