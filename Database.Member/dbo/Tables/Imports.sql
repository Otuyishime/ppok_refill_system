﻿CREATE TABLE [dbo].[Imports]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date_Uploaded] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UserName]  VARCHAR(256) NOT NULL,
	[Type] VARCHAR(256) NOT NULL DEFAULT 'Daily',
	[FileName] VARCHAR(256) NOT NULL
)
