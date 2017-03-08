CREATE TABLE Template (
	[Id] int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	[Comm_Type_Id] int NOT NULL,
	[Temp_Type_Id] int NOT NULL,
	[Temp_Message] VARCHAR(MAX),
	FOREIGN KEY ([Comm_Type_Id]) REFERENCES CommunicationType(ID),
	FOREIGN KEY ([Temp_Type_Id]) REFERENCES TemplateType([Id])
);