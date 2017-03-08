/* medication table */
CREATE TABLE Medication (
	ID int IDENTITY (1, 1) PRIMARY KEY NOT NULL,
	med_name VARCHAR(25) NOT NULL,
	med_description VARCHAR(128)
);