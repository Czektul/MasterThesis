CREATE TABLE Users(
	Id int PRIMARY KEY IDENTITY(1,1),
	Name varchar(100) NOT NULL,
	FirstName varchar(100),
	LastName varchar(100),
	RoomId int NOT NULL
);

CREATE TABLE Rooms(
	Id int PRIMARY KEY IDENTITY(1,1),
	Number varchar(100) NOT NULL
);

CREATE TABLE Projects(
	Id int PRIMARY KEY IDENTITY(1,1),
	Name varchar(100) NOT NULL

)

CREATE TABLE ProjectsUsers(
	ProjectId int NOT NULL,
	UserId int NOT NULL
)


INSERT INTO Users (Name, FirstName, LastName, RoomId) VALUES ('mkowalski', 'Marek', 'Kowalski', 1);
INSERT INTO Users (Name, FirstName, LastName, RoomId) VALUES ('jkoziol', 'Joanna', 'Kozioł', 1);
INSERT INTO Users (Name, FirstName, LastName, RoomId) VALUES ('lmakowski', 'Leszek', 'Makowski', 2);

INSERT INTO Rooms (Number) VALUES ('1');
INSERT INTO Rooms (Number) VALUES ('2');

INSERT INTO Projects (Name) VALUES ('Projekt Główny');
INSERT INTO Projects (Name) VALUES ('Projekt Poboczny');

INSERT INTO ProjectsUsers(ProjectId, UserId) VALUES (1, 1);
INSERT INTO ProjectsUsers(ProjectId, UserId) VALUES (1, 2);
INSERT INTO ProjectsUsers(ProjectId, UserId) VALUES (1, 3);
INSERT INTO ProjectsUsers(ProjectId, UserId) VALUES (2, 2);