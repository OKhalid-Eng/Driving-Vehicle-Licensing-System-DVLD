CREATE TABLE LicenseClasses
(
    LicenseClassID INT IDENTITY(1,1),

    ClassName NVARCHAR(50) NOT NULL,

    ClassDescription NVARCHAR(500) NOT NULL,

    MinimumAllowedAge TINYINT NOT NULL,

    DefaultValidityLength TINYINT NOT NULL,

    ClassFees SMALLMONEY NOT NULL,

    CONSTRAINT PK_LicenseClasses
        PRIMARY KEY (LicenseClassID)
);