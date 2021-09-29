create DATABASE AlifBank

USE AlifBank

create TABLE Accounts (
    Id INT IDENTITY PRIMARY KEY NOT NULL,
    Login NVARCHAR(200) NOT NULL,
    Password NVARCHAR(200) NOT NULL,
    LastName NVARCHAR(200) NOT NULL,
    FirstName NVARCHAR(200) NOT NULL,
    Age INT NOT NULL,
    Gender INT NOT NULL,
    Is_Admin INT NOT NULL,
)

create TABLE Transactions(
    Id INT IDENTITY PRIMARY KEY NOT NULL,
    Account_Id INT REFERENCES Accounts(Id) NOT NULL,
    Amount DECIMAL(20, 2) NOT NULL,
    Type NVARCHAR(200) NOT NULL,
    Limit INT NOT NULL,
    Created_At DATETIME NOT NULL,
)
