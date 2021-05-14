GO
CREATE TABLE Logs(
	id INT IDENTITY PRIMARY KEY,
	type VARCHAR(40),
	table_name VARCHAR(40),
	execution_date DATETIME
);

GO
CREATE OR ALTER PROCEDURE uspAddLoan (@ISBN BIGINT, @CId INT, @LoanDate DATE, @DueDate DATE, @IsReturned BIT, @RestitutionDate DATE)
AS
	IF EXISTS (SELECT * 
				FROM Loans L 
				WHERE L.ISBN = @ISBN AND L.CId = @CId)
	BEGIN 
		RAISERROR('Loan already exists', 14, 1)
	END
	INSERT INTO Loans VALUES (@CId, @ISBN, @LoanDate, @DueDate, @IsReturned, @RestitutionDate)
	INSERT INTO Logs VALUES('add', 'Loans', GETDATE())
	
GO