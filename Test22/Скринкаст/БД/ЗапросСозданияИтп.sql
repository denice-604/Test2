drop database Test2

create database Test2


CREATE TABLE Files
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(90)
);

CREATE TABLE Class
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200),
    FileID INT FOREIGN KEY REFERENCES Files(ID)
);

CREATE TABLE Account
(
    ID INT PRIMARY KEY IDENTITY(1,1),
	Bank INT,
    IncomingBalanceActive FLOAT,
    IncomingBalancePassive FLOAT,
    DebitTurnover FLOAT,
    CreditTurnover FLOAT,
    InitialBalanceActive FLOAT,
    InitialBalancePassive FLOAT,
    ClassID INT FOREIGN KEY REFERENCES Class(ID)
);


Select * from Account
Select * from Class
Select * from Files
 


delete from Account
delete from Class
delete from Files