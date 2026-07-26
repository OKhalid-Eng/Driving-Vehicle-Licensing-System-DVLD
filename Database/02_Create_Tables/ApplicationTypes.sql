CREATE TABLE ApplicationTypes
(
    ApplicationTypeID INT IDENTITY(1,1),

    ApplicationTypeTitle VARCHAR(100) NOT NULL,

    Fees SMALLMONEY NOT NULL

    CONSTRAINT PK_AppType
        PRIMARY KEY (ApplicationTypeID)
);