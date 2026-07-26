
CREATE TABLE Applications
(
    ApplicationID INT IDENTITY(1,1),

    PersonID INT NOT NULL,

    ApplicationTypeID INT NOT NULL,

    ApplicationDate DATE NOT NULL,

    PaidApplicationFees DECIMAL(10,2) NOT NULL,

    ApplicationStatus TINYINT NOT NULL,

    LastStatusDate DATE NOT NULL,

    CreatedByUserID int NOT NULL,


    CONSTRAINT PK_Applications
        PRIMARY KEY (ApplicationID),

    CONSTRAINT FK_Applications_Persons
        FOREIGN KEY (PersonID)
        REFERENCES Persons(PersonID),

	CONSTRAINT FK_Applications_CreatedByUser
        FOREIGN KEY (CreatedByUserID)
        REFERENCES Users(UserID),

    CONSTRAINT FK_Applications_ApplicationTypes
        FOREIGN KEY (ApplicationTypeID)
        REFERENCES ApplicationTypes(ApplicationTypeID)
);