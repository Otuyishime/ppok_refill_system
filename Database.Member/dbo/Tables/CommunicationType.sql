CREATE TABLE CommunicationType(
	ID int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	comm_type_name VARCHAR(25) NOT NULL,
	comm_type_description VARCHAR(250)
);