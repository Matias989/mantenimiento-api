CREATE DATABASE MantenimientoApi
GO
USE MantenimientoApi
GO
CREATE TABLE Permissions
(
	Id INT IDENTITY NOT NULL,
	Name NVARCHAR(60) NOT NULL,
	Description NVARCHAR(160) NOT NULL,
	CONSTRAINT PK_Permissions_Id PRIMARY KEY (Id)
)
GO
CREATE TABLE Views
(
	Id INT IDENTITY NOT NULL,
	Name NVARCHAR(60) NOT NULL,
	Description NVARCHAR(160) NOT NULL,
	CONSTRAINT PK_Views_Id PRIMARY KEY (Id)
)
GO
CREATE TABLE ViewsPermissions
(
	Id INT IDENTITY NOT NULL,
	IdPermission INT NOT NULL,
	IdView INT NOT NULL,
	CONSTRAINT PK_ViewsPermissions_Id PRIMARY KEY (Id),
	CONSTRAINT FK_ViewsPermissions_IdPermission FOREIGN KEY (IdPermission) REFERENCES Permissions(Id),
	CONSTRAINT FK_ViewsPermissions_IdView FOREIGN KEY (IdView) REFERENCES Views(Id)
)
GO
CREATE TABLE Rols
(
	Id INT IDENTITY NOT NULL,
	Name NVARCHAR(60) NOT NULL,
	Description NVARCHAR(160) NOT NULL,
	CONSTRAINT PK_Rols_Id PRIMARY KEY (Id)
)
GO
CREATE TABLE RolsViewsPermissions
(
	Id INT IDENTITY NOT NULL,
	IdRol INT NOT NULL,
	IdViewPermission INT NOT NULL,
	CONSTRAINT PK_RolsPermissions_Id PRIMARY KEY (Id),
	CONSTRAINT FK_RolsPermissions_IdRol FOREIGN KEY (IdRol) REFERENCES Rols(Id),
	CONSTRAINT FK_RolsPermissions_IdViewPermission FOREIGN KEY (IdViewPermission) REFERENCES ViewsPermissions(Id)
)
GO
CREATE TABLE Users
(
	Id INT IDENTITY NOT NULL,
	IdRol INT NOT NULL,
	Name NVARCHAR(60) NOT NULL,
	Password NVARCHAR(MAX) NOT NULL,
	Email NVARCHAR(120) NOT NULL,
	Salt VARBINARY(MAX) NULL,
	Active BIT NOT NULL,
	CONSTRAINT PK_Users_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Users_IdRol FOREIGN KEY (IdRol) REFERENCES Rols(Id),
	CONSTRAINT UQ_Users_Email UNIQUE (Email)
)
GO
CREATE TABLE WorkOrders
(
	Id INT IDENTITY NOT NULL,
	IdUserCreator INT NOT NULL,
	IdUserAsigned INT,
	Progress INT NOT NULL,
	Observation NVARCHAR(MAX) NOT NULL,
	CreationDate DATETIME NOT NULL,
	FinishDate DATETIME,
	Priority INT,
	CONSTRAINT PK_WorkOrders_Id PRIMARY KEY (Id),
	CONSTRAINT FK_WorkOrders_IdUserCreator FOREIGN KEY (IdUserCreator) REFERENCES Users(Id),
	CONSTRAINT FK_WorkOrders_IdUserAsigned FOREIGN KEY (IdUserAsigned) REFERENCES Users(Id)
)
GO
CREATE TABLE Observations
(
	Id INT IDENTITY NOT NULL,
	IdUser INT NOT NULL,
	IdWorkOrder INT NOT NULL,
	Msj NVARCHAR(MAX) NOT NULL,
	CreationDate DATETIME NOT NULL,
	CONSTRAINT PK_Observations_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Observations_IdUser FOREIGN KEY (IdUser) REFERENCES Users(Id),
	CONSTRAINT FK_Observations_IdWorkOrder FOREIGN KEY (IdWorkOrder) REFERENCES WorkOrders(Id)
)
GO
CREATE TABLE Pictures
(
	Id INT IDENTITY NOT NULL,
	IdWorkOrder INT NOT NULL,
	Title NVARCHAR(MAX) NOT NULL,
	Path NVARCHAR(MAX) NOT NULL,
	Cover BIT NOT NULL,
	CONSTRAINT PK_Pictures_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Pictures_IdWorkOrder FOREIGN KEY (IdWorkOrder) REFERENCES WorkOrders(Id)
)