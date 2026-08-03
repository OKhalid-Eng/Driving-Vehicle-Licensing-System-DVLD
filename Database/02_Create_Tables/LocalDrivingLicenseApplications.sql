CREATE TABLE LocalDrivingLicenseApplications
(
    LocalDrivingLicenseApplicationID INT IDENTITY(1,1) NOT NULL,

    ApplicationID INT NOT NULL,

    LicenseClassID INT NOT NULL,

    CONSTRAINT PK_DrivingLicsenseApplications
        PRIMARY KEY (LocalDrivingLicenseApplicationID),

    CONSTRAINT FK_DrivingLicsenseApplications_Applications
        FOREIGN KEY (ApplicationID)
        REFERENCES Applications(ApplicationID),

    CONSTRAINT FK_DrivingLicsenseApplications_LicenseClasses
        FOREIGN KEY (LicenseClassID)
        REFERENCES LicenseClasses(LicenseClassID)
);