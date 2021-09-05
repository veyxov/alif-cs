use ismoil

--• Accounts.Client_Id => Clients.Id
--• Accounts.Currency_Id => Currencies.Id

create table Clients (
	Id int identity primary key not null,
	LastName nvarchar(200) not null,
	FirstName nvarchar(200) not null,
	MiddleName nvarchar(200) null,
	BirthDate datetime not null,
	Created_At datetime not null
);

create table Currencies (
	Id int identity primary key not null,
	Currency nvarchar(200) not null
);

create table Accounts (
	Id int identity primary key not null,
	Client_Id int not null,
	Account_Number nvarchar(200) not null,
	Currency_Id int not null,
	Created_At datetime not null,
	Updated_At datetime not null,

	AccountsClientId int references Clients (Id) not null,
	AccoutnsCurrencyId int references Currencies (Id) not null
);