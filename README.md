# MoneyManagementApp
-- Database for Final Project PRN221 - Web quản lý tiền - MoneyManagement
Create database MoneyManagementV2

-------------------------------------------------------------------------
create table Saver(
	userId int primary key identity(1,1),
	username varchar(50) unique, 
	email varchar(50) unique,
	password varchar(32) not null,
	avatar nvarchar(max)
)

Create table  Cate(
	cateId int primary key identity(1,1), 
	cateName nvarchar(50),
	icon varchar(50),
	color varchar(50),
	type bit default 0
)

Create table MAccount(
	accountId int primary key identity(1,1),
	accountName varchar(50),
	[money] money,
	icon varchar(50),
	color varchar(50),
	userId int foreign key references Saver(userId)
)

create table Transction(
	id int primary key identity(1,1),
	userId int foreign key references Saver(userId),
	cateId int foreign key references Cate(cateId),
	accountId int foreign key references MAccount(accountId),
	[money] money,
	type bit default 0,
	[datetime]  smalldatetime default GETDATE(),
	note nvarchar(max)
)

create table Mesage(
	id int primary key identity(1,1),
	userId1 int foreign key references Saver(userId),
	userId2 int foreign key references Saver(userId),
	[datetime]  smalldatetime default GETDATE(),
	note nvarchar(max)
)
