create DATABASE AlifBank

USE AlifBank

create TABLE Accounts (
    Id INT IDENTITY PRIMARY KEY NOT NULL,
    Login nvarchar(10) NOT NULL,
    Password nvarchar(200) NOT NULL,
    LastName nvarchar(20) NOT NULL,
    FirstName nvarchar(20) NOT NULL,
    Age INT NOT NULL,
    Gender INT NOT NULL,
)

create TABLE Transactions(
    Id INT IDENTITY PRIMARY KEY NOT NULL,
    Account_Id INT REFERENCES Accounts(Id) NOT NULL,
    Amount decimal(20, 2) not null,
)
