--T2
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
	SELECT *
	FROM ingrediente
	WAITFOR DELAY '00:00:05'
	SELECT *
	FROM ingrediente
COMMIT TRAN
 

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRAN
	SELECT *
	FROM ingrediente
	WAITFOR DELAY '00:00:05'
	SELECT *
	FROM ingrediente
COMMIT TRAN
