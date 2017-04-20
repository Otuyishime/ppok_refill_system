CREATE TABLE [dbo].[DuePickUps]
(
    [PatientId] INT NOT NULL, 
    [PatientName] NVARCHAR(256) NOT NULL, 
    [MedicineName] NVARCHAR(256) NOT NULL, 
    [GuidRand] NVARCHAR(MAX) NOT NULL, 
    [IsPickUpReady] BIT NOT NULL, 
    [PickupId] INT IDENTITY(1,1) NOT NULL, 
    CONSTRAINT [PK_DuePickUps] PRIMARY KEY ([PickupId]) 
)
