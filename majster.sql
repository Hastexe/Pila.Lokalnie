create database Majster
go
use Majster
go

create table Users(
Login varchar(255) NOT NULL PRIMARY KEY,
Password varchar(255) NOT NULL,
Mail varchar(255) NOT NULL,
);

create table Old_Pass(
ID int NOT NULL PRIMARY KEY,
Login varchar(255) NOT NULL,
Password varchar(255) NOT NULL,
);

create table Users_Data(
Name varchar(50),
Surname varchar(50),
Adress varchar(50),
City varchar(50),
Login varchar(255) NOT NULL PRIMARY KEY,
Mail varchar(320) DEFAULT NULL,
Number varchar(9) DEFAULT NULL,
);

create table Categories(
Tag varchar(3) NOT NULL PRIMARY KEY,
Name varchar(50) NOT NULL,
);

Create table Advertisement(
ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
Customer varchar(255) NOT NULL,
Specialist varchar(255) NOT NULL,
Date smalldatetime NOT NULL,
Text text,
Price smallmoney,
Category varchar(3) NOT NULL,
Place varchar(50) NOT NULL,
Picture varchar(155), /*tutaj bêdzie œcie¿ka do zdjêcia*/
Status varchar(25) DEFAULT 'Wolne',
);

create table Permissions(
Login varchar(255) NOT NULL PRIMARY KEY,
Admin bit, 	
);

create table Comments(
ID int NOT NULL PRIMARY KEY,
Customer varchar(255) NOT NULL,
Specialist varchar(255) NOT NULL, /*odbiorca komentarza*/
Text text,
Stars int,
Date smalldatetime,
);

ALTER TABLE [dbo].[Advertisement]  WITH CHECK ADD  CONSTRAINT [FK_Advertisement_Users] FOREIGN KEY([Customer])
REFERENCES [dbo].[Users] ([Login])
GO
ALTER TABLE [dbo].[Advertisement] CHECK CONSTRAINT [FK_Advertisement_Users]
GO
ALTER TABLE [dbo].[Advertisement]  WITH CHECK ADD  CONSTRAINT [FK_Advertisement_Users1] FOREIGN KEY([Specialist])
REFERENCES [dbo].[Users] ([Login])
GO
ALTER TABLE [dbo].[Advertisement] CHECK CONSTRAINT [FK_Advertisement_Users1]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([Specialist])
REFERENCES [dbo].[Users] ([Login])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users1] FOREIGN KEY([Customer])
REFERENCES [dbo].[Users] ([Login])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users1]
GO
ALTER TABLE [dbo].[Old_Pass]  WITH CHECK ADD  CONSTRAINT [FK_Old_Pass_Permissions] FOREIGN KEY([Login])
REFERENCES [dbo].[Permissions] ([Login])
GO
ALTER TABLE [dbo].[Old_Pass] CHECK CONSTRAINT [FK_Old_Pass_Permissions]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Users] FOREIGN KEY([Login])
REFERENCES [dbo].[Users] ([Login])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Users]
GO
ALTER TABLE [dbo].[Users_Data]  WITH CHECK ADD  CONSTRAINT [FK_Users_Data_Users] FOREIGN KEY([Login])
REFERENCES [dbo].[Users] ([Login])
GO
ALTER TABLE [dbo].[Users_Data] CHECK CONSTRAINT [FK_Users_Data_Users]
GO


select * from Users
insert into Users values ('Majster','123123','mail@mail.pl')


select * from Advertisement