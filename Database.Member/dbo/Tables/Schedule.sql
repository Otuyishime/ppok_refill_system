/* schedule table: this table depends on both users and prescription tables */
CREATE TABLE Schedule (
	[Id] int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	[Prescription_Id] int NOT NULL,
	[Future_Refill_Date] DATE NOT NULL, 
    [Approved] BIT NULL DEFAULT 0, 
    FOREIGN KEY ([Prescription_Id]) REFERENCES Prescription([Id])
);