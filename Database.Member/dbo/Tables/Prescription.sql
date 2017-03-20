/* prescription table */
CREATE TABLE Prescription (
	[Id] int IDENTITY (1, 1) PRIMARY KEY NOT NULL,
	[User_Id] int NOT NULL,
	[Medication_Id] VARCHAR(50) NOT NULL,
	[Refills_Left] numeric (4, 0) NOT NULL,
	[Days_Supply] numeric (4,0) NOT NULL,
	[Last_Date_Filled] date NOT NULL,
	FOREIGN KEY ([User_Id]) REFERENCES Member(Id),
	FOREIGN KEY ([Medication_Id]) REFERENCES Medication(ID)
);