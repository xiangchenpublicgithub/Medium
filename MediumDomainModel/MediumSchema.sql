﻿CREATE DATABASE [Medium]
GO

USE [Medium]
GO

CREATE TABLE [Posts] (
	[Id] INT IDENTITY PRIMARY KEY,
	[Slug] NVARCHAR(250) NOT NULL UNIQUE,
	[Title] NVARCHAR(200) NOT NULL,
	[Body] NVARCHAR(MAX) NOT NULL,
	[Published] BIT NOT NULL,
	[PublishedAt] DATETIME NOT NULL,
)
GO