CREATE TABLE Persons
(
    PersonID INT IDENTITY(1,1),

    NationalID NVARCHAR(20) NOT NULL UNIQUE,

    FirstName NVARCHAR(50) NOT NULL,
    SecondName NVARCHAR(50) NULL,
    ThirdName NVARCHAR(50) NULL,
    LastName NVARCHAR(50) NOT NULL,

    DateOfBirth DATE NOT NULL,

    NationalityCountryID int NOT NULL,

    Address NVARCHAR(200) NOT NULL,

    PhoneNo VARCHAR(20) NOT NULL,

    Email VARCHAR(100) NULL,

    ImagePath NVARCHAR(250) NULL,
	
    CONSTRAINT PK_Persons
        PRIMARY KEY (PersonID),

    CONSTRAINT FK_NationalityCountry_Persons
        FOREIGN KEY (NationalityCountryID)
        REFERENCES Countries(CountryID),

);