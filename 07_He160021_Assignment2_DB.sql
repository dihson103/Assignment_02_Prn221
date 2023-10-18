CREATE DATABASE Prn221_Assignment02_07

CREATE TABLE Customers (
	CustomerID	INT				PRIMARY KEY IDENTITY (1, 1),
	Username	VARCHAR(50)		UNIQUE NOT NULL,
	[Password]	VARCHAR(50)		NOT NULL,
	FullName	VARCHAR(50)		NOT NULL,
	[Address]	VARCHAR(255)	NOT NULL,
	Phone		VARCHAR(10)		NOT NULL,
	[Type]		INT				NOT NULL
)

CREATE TABLE Categories (
	CategoryID		INT				PRIMARY KEY IDENTITY (1, 1),
	CategoryName	VARCHAR(255)	NOT NULL,
	[Description]	VARCHAR(255)	NOT NULL
)

CREATE TABLE Suppliers (
	SupplierID	INT				PRIMARY KEY IDENTITY(1, 1),
	CompanyName VARCHAR(255)	NOT NULL,
	[Address]	VARCHAR(255)	NOT NULL,
	Phone		VARCHAR(10)		NOT NULL
)

CREATE TABLE Products(
	ProductID		INT				PRIMARY KEY IDENTITY(1, 1),
	SupplierID		INT				REFERENCES Suppliers(SupplierID),
	CategoryID		INT				REFERENCES Categories(CategoryID),
	ProductName		VARCHAR(255)	NOT NULL,
	QuantityPerUnit INT				NOT NULL,
	UnitPrice		MONEY			NOT NULL,
	ProductImage	VARCHAR(255)	NOT NULL
)

CREATE TABLE Orders(
	OrderID			INT				PRIMARY KEY IDENTITY(1, 1),
	CustomerID		INT				REFERENCES Customers(CustomerID),
	OrderDate		DATE			NOT NULL,
	RequiredDate	DATE,
	ShippedDate		DATE,
	Freight			INT,
	ShipAddress		VARCHAR(255)	NOT NULL
)

CREATE TABLE OrderDetails(
	OrderID		INT		REFERENCES Orders(OrderID),
	ProductID	INT		REFERENCES Products(ProductID),
	UnitPrice	MONEY	NOT NULL,
	Quantity	INT		NOT NULL,
	PRIMARY KEY (OrderID, ProductID)
)