﻿CREATE TABLE [dbo].[Member] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    [DateBirth] DATE NULL, 
    [Address] NVARCHAR(200) NULL, 
    [Active] BIT NULL,  
    [CommunicationType] INT NULL DEFAULT 2, 
    CONSTRAINT [PK_dbo.Member] PRIMARY KEY CLUSTERED ([Id] ASC)
);

