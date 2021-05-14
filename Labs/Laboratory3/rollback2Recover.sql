USE BookLibrary;

GO
CREATE OR ALTER FUNCTION uf_ValidateNames (@name VARCHAR(50)) RETURNS INT AS
BEGIN --check for empty name
	DECLARE @return INT
	SET @return = 1
	IF(@name = '')
		SET @return = 0
	RETURN @return
END

GO
CREATE OR ALTER FUNCTION uf_ValidateSSN (@SSN VARCHAR(14)) RETURNS INT AS
BEGIN --check for duplicate SSN
	DECLARE @return INT
	SET @return = 1
	IF EXISTS(SELECT * FROM Client c WHERE c.SSN = @SSN)
		SET @return = 0
	RETURN @return
END

GO
CREATE OR ALTER FUNCTION uf_ValidateDates (@date DATE) RETURNS INT AS
BEGIN --check if the parameter date is bigger than current date
	DECLARE @return INT
	SET @return = 1
	IF(@date >= GETDATE())
		SET @return = 0
	RETURN @return
END


GO
CREATE OR ALTER PROCEDURE uspAddClientRecoverable(@fName VARCHAR(50), @sName VARCHAR(50), @SSN VARCHAR(14), @address VARCHAR(100), @phoneNumber VARCHAR(12), @email VARCHAR(50), @regDate DATE, @CGId INT)
AS
	BEGIN TRAN
		BEGIN TRY
			IF(dbo.uf_ValidateNames(@fName) <> 1 OR dbo.uf_ValidateNames(@sName) <> 1 OR dbo.uf_ValidateSSN(@SSN) <> 1 OR  dbo.uf_ValidateDates(@regDate) <> 1)
			BEGIN
				RAISERROR('Invalid client attributes!', 14, 1) --error adding
			END
			INSERT INTO Client VALUES (@fName, @sName, @SSN, @address, @phoneNumber, @email, @regDate, @CGId)
			INSERT INTO LogTable VALUES('add', 'Client', GETDATE())
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			PRINT 'Could not add this client'
		END CATCH
GO


CREATE OR ALTER FUNCTION uf_ValidateCount (@count INT) RETURNS INT AS
BEGIN --check if the count is smaller or equal than 0
	DECLARE @return INT
	SET @return = 0
	IF(@count >= 0)
		SET @return = 1
	RETURN @return
END

GO
CREATE OR ALTER FUNCTION uf_ValidatePublisherId(@publisherId INT) RETURNS INT AS
BEGIN --check if the publisher id exists in their table
	DECLARE @return INT
	SET @return = 1
	IF NOT EXISTS(SELECT * FROM PublishingHouse p WHERE p.Id = @publisherId)
		SET @return = 0
	RETURN @return
END

GO
CREATE OR ALTER FUNCTION uf_ValidateYearOfPublication (@yearPublication INT) RETURNS INT AS
BEGIN --check if the publication year is bigger than the current year
	DECLARE @return INT
	SET @return = 0
	IF(@yearPublication <= YEAR(GETDATE()))
		SET @return = 1
	RETURN @return
END


GO
CREATE OR ALTER PROCEDURE uspAddBookRecoverable(@ISBN BIGINT, @count INT, @publisherId INT, @LId INT, @categoryId INT, @author VARCHAR(30), @yearPublication INT, @coverType INT, @description VARCHAR(300))
AS
	BEGIN TRAN
		BEGIN TRY
			IF(dbo.uf_ValidateCount(@count) <> 1)
			BEGIN
				RAISERROR('Invalid count!', 14, 1) 
			END
			IF(dbo.uf_ValidatePublisherId(@publisherId) <> 1)
			BEGIN
				RAISERROR('Invalid publisher id!', 14, 1) 
			END
			IF(dbo.uf_ValidateNames(@author) <> 1)
			BEGIN
				RAISERROR('Invalid author!', 14, 1)
			END
			IF(dbo.uf_ValidateYearOfPublication(@yearPublication) <> 1) 
			BEGIN
				RAISERROR('Invalid publication year!', 14, 1)
			END

			INSERT INTO Books VALUES (@ISBN, @count, @publisherId, @LId, @categoryId, @author, @yearPublication, @coverType, @description)
			INSERT INTO LogTable VALUES('add', 'Books', GETDATE())
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			PRINT 'Could not add this book'
		END CATCH
GO

GO
CREATE OR ALTER FUNCTION uf_ValidateISBN(@ISBN INT) RETURNS INT AS
BEGIN ----check if the ISBN exists in the books table 
	DECLARE @return INT
	SET @return = 1
	IF NOT EXISTS(SELECT * FROM Books b WHERE b.ISBN = @ISBN)
		SET @return = 0
	RETURN @return
END

GO
CREATE OR ALTER PROCEDURE uspAddLoanRecoverable(@ISBN INT, @loanDate DATE, @dueDate DATE, @isReturned bit, @restitutionDate DATE)
AS
	BEGIN TRAN 
		BEGIN TRY
			IF(dbo.uf_ValidateISBN(@ISBN) <> 1 OR dbo.uf_ValidateDates(@loanDate) <> 1) 
			BEGIN
				RAISERROR('Invalid loan attributes!', 14, 1)
			END

			DECLARE @clientId INT --get the last added client id, because it is on auto increment
			SELECT @clientId = MaxClientId
			FROM (
			SELECT MAX(c.CId) as MaxClientId
			FROM Client c
			) t
	
			IF EXISTS (SELECT * FROM Loans l WHERE l.CId = @clientId AND l.ISBN = @ISBN)
			BEGIN --check for duplicate loan
			RAISERROR('Loan already exists!', 14, 1)
			END

			INSERT INTO Loans VALUES (@clientId, @ISBN, @loanDate, @dueDate, @isReturned, @restitutionDate)
			INSERT INTO LogTable VALUES ('add', 'Loan', GETDATE())
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			PRINT 'Could not add this loan'
		END CATCH
GO


GO -- each of the three transactions are independent of the others
CREATE OR ALTER PROCEDURE uspAddErrorScenarioRollback
AS
	EXEC uspAddClientRecoverable 'Ionut', 'Matei', '5000528946210', 'Cluj, Capusu Mare', '073297392', 'ionut@gmail.com', '2020-10-10', 1 --fails
	EXEC uspAddBookRecoverable 946321890, 2, 1, 1, 2, 'Andrei', 2019, 2, 'Best book' --works
	EXEC uspAddLoanRecoverable 948765521, '2021-04-15', '2021-05-21', 0, NULL --fails
GO

-- these 3 will all succeed
CREATE OR ALTER PROCEDURE uspAddHappyScenarioRollback
AS
	EXEC uspAddClientRecoverable 'Ionut', 'Matei', '500012349743', 'Cluj, Capusu Mare', '073297392', 'ionut@gmail.com', '2020-10-10', 1
	EXEC uspAddBookRecoverable 946321890, 2, 1, 1, 2, 'Andrei', 2019, 2, 'Best book'
	EXEC uspAddLoanRecoverable 946321890, '2021-04-15', '2021-05-21', 0, NULL 
GO


EXEC uspAddErrorScenarioRollback

--undo uspAddErrorScenarioRollback
DELETE FROM Books WHERE ISBN = 946321890;

EXEC uspAddHappyScenarioRollback

SELECT * FROM Client
SELECT * FROM Books
SELECT * FROM Loans

SELECT * FROM LogTable


DELETE FROM Loans WHERE ISBN = 946321890 AND LoanDate = '2021-04-15' AND DueDate = '2021-05-21';
DELETE FROM Client WHERE FirstName = 'Ionut' AND SecondName = 'Matei';
DELETE FROM Books WHERE ISBN = 946321890;

DELETE FROM LogTable;

