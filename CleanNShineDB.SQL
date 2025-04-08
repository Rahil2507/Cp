USE [master]
GO
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = N'CleanNShineDB' OR name = N'CleanNShineDB')))
DROP DATABASE CleanNShineDB

CREATE DATABASE CleanNShineDB
GO
Use CleanNShineDB
GO

IF OBJECT_ID('User')  IS NOT NULL
DROP TABLE [User]
GO
IF OBJECT_ID('Address')  IS NOT NULL
DROP TABLE [Address]
GO
IF OBJECT_ID('Service')  IS NOT NULL
DROP TABLE [Service]
GO
IF OBJECT_ID('Booking')  IS NOT NULL
DROP TABLE [Booking]
GO
IF OBJECT_ID('Payment')  IS NOT NULL
DROP TABLE [Payment]
GO
IF OBJECT_ID('Feedback')  IS NOT NULL
DROP TABLE [Feedback]
GO
  

CREATE TABLE [User] (
    [UserId] SMALLINT CONSTRAINT pk_UserId PRIMARY KEY IDENTITY(1000,1),
    [FirstName] VARCHAR(30),
    [LastName] VARCHAR(30),
    [Email] VARCHAR(50) CONSTRAINT chk_Email UNIQUE,
    [Contact] BIGINT,
    [Password] VARCHAR(20),
    [DateOfBirth] DATE,
    [Gender] VARCHAR(6) CONSTRAINT chk_Gender CHECK ([Gender] IN ('Male','Female','Others')),
    [Role] VARCHAR(9) CONSTRAINT chk_Role CHECK ([Role] IN ('Customer','Attendant'))
);
GO
 

CREATE TABLE [Address] (
    [AddressId] SMALLINT CONSTRAINT pk_AddressId PRIMARY KEY IDENTITY(1000,1),
    [UserId] SMALLINT CONSTRAINT fk_AddressUserId FOREIGN KEY REFERENCES [User](UserId),
    [HouseNumber] INT,
    [AreaName] VARCHAR(30),
    [City] VARCHAR(30),
    [PinCode] INT
);
GO
 

CREATE TABLE [Service] (
    [ServiceId] SMALLINT CONSTRAINT pk_ServiceId PRIMARY KEY IDENTITY(1000,1),
    [ServiceType] VARCHAR(7) CONSTRAINT chk_ServiceType CHECK ([ServiceType] IN ('Indoor', 'Outdoor')),
    [ServiceName] VARCHAR(30),
    [Description] VARCHAR(200),
    [Duration] TINYINT,
    [SuppliesRequired] VARCHAR(100),
    [ServiceCharge] MONEY,
    [Availability] BIT
);
GO
 

CREATE TABLE [Booking] (
    [BookingId] SMALLINT CONSTRAINT pk_BookingId PRIMARY KEY IDENTITY(1000,1),
    [ServiceId] SMALLINT CONSTRAINT fk_ServiceId FOREIGN KEY REFERENCES [Service](ServiceId),
    [UserId] SMALLINT CONSTRAINT fk_BookingUserId FOREIGN KEY REFERENCES [User](UserId),
    [AddressId] SMALLINT CONSTRAINT fk_AddressId FOREIGN KEY REFERENCES [Address](AddressId),
    [AttendantId] SMALLINT CONSTRAINT fk_AttendantId FOREIGN KEY REFERENCES [User](UserId),
    [DateOfService] DATE,
    [DeepCleaning] BIT,
    [RepeatFrequency] TINYINT,
    [Instructions] VARCHAR(100),
    [CustomerSupplies] VARCHAR(100),
    [ServiceStatus] VARCHAR(20)
);
GO
 

CREATE TABLE [Payment] (
    [PaymentId] SMALLINT CONSTRAINT pk_PaymentId PRIMARY KEY IDENTITY(1000,1),
    [BookingId] SMALLINT CONSTRAINT fk_PaymentBookingId FOREIGN KEY REFERENCES [Booking](BookingId),
    [PaymentType] VARCHAR(11) CONSTRAINT chk_PaymentType CHECK ([PaymentType] IN ('Credit Card', 'Debit Card', 'UPI')),
    [CardHolderName] VARCHAR(50),
    [CardNumber] BIGINT,
    [Cvv] SMALLINT,
    [UpiId] VARCHAR(30)
);
GO
 

CREATE TABLE [Feedback] (
    [FeedbackId] SMALLINT CONSTRAINT pk_FeedbackId PRIMARY KEY IDENTITY(1000,1),
    [BookingId] SMALLINT CONSTRAINT fk_FeedbackBookingId FOREIGN KEY REFERENCES [Booking](BookingId),
    [Ratings] TINYINT,
    [Message] VARCHAR(50)
);
GO
 

 
INSERT INTO [User] (FirstName, LastName, Email, Contact, Password, DateOfBirth, Gender, Role) VALUES
('John', 'Doe', 'john@example.com', 123456789, 'pass123', '1990-05-15', 'Male', 'Customer'),
('Jane', 'Smith', 'jane@example.com', 234567890, 'word456', '1985-08-20', 'Female', 'Attendant'),
('Bob', 'Lee', 'bob@example.com', 345678901, 'secret789', '1992-12-01', 'Male', 'Customer'),
('Sara', 'Khan', 'sara@example.com', 456789012, 'pass000', '1993-03-11', 'Female', 'Attendant');
GO
INSERT INTO [Address] (UserId, HouseNumber, AreaName, City, PinCode) VALUES
(1000, 101, 'Maple Street', 'New York', 100001),
(1001, 202, 'Pine Lane', 'Los Angeles', 900002),
(1002, 303, 'Cedar Ave', 'Chicago', 600003),
(1003, 404, 'Elm Road', 'Houston', 770004);
GO
INSERT INTO [Service] (ServiceType, ServiceName, Description, Duration, SuppliesRequired, ServiceCharge, Availability) VALUES
('Indoor', 'Mopping', 'Floor mopping and cleaning', 2, 'Bucket, Mop', 100.00, 1),
('Outdoor', 'Car Wash', 'Exterior car cleaning', 1, 'Hose, Shampoo', 150.00, 1),
('Indoor', 'Window Cleaning', 'Glass surface cleaning', 2, 'Sponge, Cleaner', 120.00, 0);
GO
INSERT INTO [Booking] (ServiceId, UserId, AddressId, AttendantId, DateOfService, DeepCleaning, RepeatFrequency, Instructions, CustomerSupplies, ServiceStatus) VALUES
(1000, 1000, 1000, 1001, '2025-04-10', 1, 0, 'Focus on corners', 'Own Mop', 'Scheduled'),
(1001, 1002, 1002, 1003, '2025-04-11', 0, 2, 'Use eco-friendly', 'None', 'Completed'),
(1002, 1000, 1000, 1003, '2025-04-12', 1, 1, 'Be careful with glass', 'Cleaner', 'InProgress');
GO
INSERT INTO [Payment] (BookingId, PaymentType, CardHolderName, CardNumber, Cvv, UpiId) VALUES
(1000, 'Credit Card', 'John Doe', 1234567890123456, 123, NULL),
(1001, 'UPI', NULL, NULL, NULL, 'bob@upi'),
(1002, 'Debit Card', 'Sara Khan', 6543210987654321, 456, NULL);
GO
INSERT INTO [Feedback] (BookingId, Ratings, Message) VALUES
(1000, 5, 'Great service!'),
(1001, 4, 'Satisfied overall'),
(1002, 3, 'Could be better');
GO





CREATE PROCEDURE usp_AddService
(
@ServiceType VARCHAR(7),
@ServiceName VARCHAR(30),
@Description VARCHAR(200),
@Duration TINYINT,
@SuppliesRequired VARCHAR(100),
@ServiceCharge MONEY,
@Availability BIT
)
AS
BEGIN
DECLARE @RetVal INT
BEGIN TRY
	INSERT INTO [Service] (ServiceType, ServiceName, Description, Duration, SuppliesRequired, ServiceCharge, Availability)
	VALUES (@ServiceType, @ServiceName, @Description, @Duration, @SuppliesRequired, @ServiceCharge, @Availability)
	SET @RetVal = 1
	SELECT @RetVal
END TRY
BEGIN CATCH
	SET @RetVal = -99
	SELECT @RetVal
END CATCH
END
GO



CREATE FUNCTION ufn_GetAllServices(@ServiceType VARCHAR(7))
RETURNS TABLE
AS
RETURN
(
	SELECT ServiceId, ServiceType, ServiceName, Description, Duration, SuppliesRequired, ServiceCharge, Availability
	FROM [Service]
    WHERE ServiceType = @ServiceType
)
GO


SELECT * FROM [User]
SELECT * FROM [Address]
SELECT * FROM [Service]
SELECT * FROM [Booking]
SELECT * FROM [Payment]
SELECT * FROM [Feedback]