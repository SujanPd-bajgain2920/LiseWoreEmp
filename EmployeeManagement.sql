Create database LiseWoreEmp

use LiseWoreEmp

CREATE TABLE UserList(
    UserId SMALLINT PRIMARY KEY NOT NULL,
    EmailAddress NVARCHAR(50) NOT NULL UNIQUE,
    UserPassword NVARCHAR(MAX) NOT NULL,
);

CREATE TABLE Employees (
    EmployeeId SMALLINT PRIMARY KEY,
    FirstName NVARCHAR(25) NOT NULL,
	MiddleName NVARCHAR(25),
    LastName NVARCHAR(25) NOT NULL,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NOT NULL UNIQUE,
    Department NVARCHAR(25),
	Position NVARCHAR(25),
    HireDate DATE,
	AddedBy SMALLINT FOREIGN KEY REFERENCEs UserList(UserId) NOT NULL
);