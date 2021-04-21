USE BookLibrary;

CREATE TABLE LogTable(
Lid INT IDENTITY PRIMARY KEY,
TypeOperation VARCHAR(50),
TableOperation VARCHAR(50),
ExecutionDate DATETIME)

GO
CREATE OR ALTER PROCEDURE uspAddClient(@fName VARCHAR(50), @sName VARCHAR(50), @SSN VARCHAR(14), @address VARCHAR(100), @phoneNumber VARCHAR(12), @email VARCHAR(50), @regDate DATE, @CGId INT)
AS
	IF EXISTS(SELECT * FROM Client c WHERE c.SSN = @SSN)
	BEGIN
		RAISERROR('Client already exists!', 14, 1) --check for duplicate SSN
	END
	INSERT INTO Client VALUES (@fName, @sName, @SSN, @address, @phoneNumber, @email, @regDate, @CGId)
	INSERT INTO LogTable VALUES('add', 'Client', GETDATE())
GO

CREATE OR ALTER FUNCTION uf_ValidateCount (@count INT) RETURNS INT AS
BEGIN
DECLARE @return INT
SET @return = 0
IF(@count >= 0)
SET @return = 1
RETURN @return
END

GO
CREATE OR ALTER PROCEDURE uspAddBook(@ISBN BIGINT, @count INT, @publisherId INT, @LId INT, @categoryId INT, @author VARCHAR(30), @yearPublication INT, @coverType INT, @description VARCHAR(300))
AS
	IF(dbo.uf_ValidateCount(@count) <> 1)
	BEGIN
		RAISERROR('Count cannot be negative!', 14, 1) --validate count
	END
	INSERT INTO Books VALUES (@ISBN, @count, @publisherId, @LId, @categoryId, @author, @yearPublication, @coverType, @description)
	INSERT INTO LogTable VALUES('add', 'Books', GETDATE())
GO

CREATE OR ALTER PROCEDURE uspAddLoan(@ISBN INT, @loanDate DATE, @dueDate DATE, @isReturned bit, @restitutionDate DATE)
AS
	DECLARE @clientId INT
	SELECT @clientId = MaxClientId
	FROM (
		SELECT MAX(c.CId) as MaxClientId
		FROM Client c
	) t
	
	IF NOT EXISTS (SELECT * FROM Books b WHERE b.ISBN = @ISBN)
	BEGIN
		RAISERROR('Invalid book ISBN!', 14, 1)
	END
	IF EXISTS (SELECT * FROM Loans l WHERE l.CId = @clientId AND l.ISBN = @ISBN)
	BEGIN
		RAISERROR('Loan already exists!', 14, 1)
	END
	INSERT INTO Loans VALUES (@clientId, @ISBN, @loanDate, @dueDate, @isReturned, @restitutionDate)
	INSERT INTO LogTable VALUES ('add', 'Loan', GETDATE())
GO

GO
CREATE OR ALTER PROCEDURE uspAddErrorScenario
AS
	BEGIN TRAN
	BEGIN TRY
		EXEC uspAddClient 'Ionut', 'Matei', '5000528946210', 'Cluj, Capusu Mare', '073297392', 'ionut@gmail.com', '2020-10-10', 1
		EXEC uspAddBook 946321890, 2, 1, 1, 2, 'Andrei', 2019, 2, 'Best book'
		EXEC uspAddLoan 946321890, '2021-04-15', '2021-05-21', 0, NULL
		COMMIT TRAN
	END TRY
	BEGIN CATCH -- if one transaction fails, rollback everything
		ROLLBACK TRAN
		RETURN
	END CATCH
GO

CREATE OR ALTER PROCEDURE uspAddHappyScenario
AS
	BEGIN TRAN
	BEGIN TRY
		EXEC uspAddClient 'Ionut', 'Matei', '500012349743', 'Cluj, Capusu Mare', '073297392', 'ionut@gmail.com', '2020-10-10', 1
		EXEC uspAddBook 946321890, 2, 1, 1, 2, 'Andrei', 2019, 2, 'Best book'
		EXEC uspAddLoan 946321890, '2021-04-15', '2021-05-21', 0, NULL 
		COMMIT TRAN
	END TRY
	BEGIN CATCH -- if one transaction fails, rollback everything
		PRINT 'ERROR'
		ROLLBACK TRAN
		RETURN
	END CATCH
GO

EXEC uspAddErrorScenario

EXEC uspAddHappyScenario

SELECT * FROM Client
SELECT * FROM Books
SELECT * FROM Loans

SELECT * FROM LogTable



DELETE FROM Client WHERE CId = 18;
DELETE FROM Loans WHERE LoanId = 8;
DELETE FROM Books WHERE ISBN = 946321890;

DELETE FROM LogTable;