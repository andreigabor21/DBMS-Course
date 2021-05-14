USE BookLibrary;

--Unrepeatable reads (unsolved)
--A row read by a transaction is changed by another transaction while the reader is in progress
--get different values when reading

--T2: select + delay + select
-- READ COMMITTED: no exclusive lock
-- here we have two different results
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRAN
	SELECT *
	FROM PublishingHouse
	WAITFOR DELAY '00:00:05'
	SELECT *
	FROM PublishingHouse
COMMIT TRAN


--Unrepeatable reads (solved)
--only the result before update in both selects
--holds shared and exclusive locks until the end of the transaction
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
	SELECT *
	FROM PublishingHouse
	WAITFOR DELAY '00:00:05'
	SELECT *
	FROM PublishingHouse
COMMIT TRAN

/*
Shared locks are placed on all data read by each statement in the transaction and are held until the 
transaction completes. This prevents other transactions from modifying any rows that have been read 
by the current transaction. Other transactions can insert new rows that match the search conditions
of statements issued by the current transaction. If the current transaction then retries the statement
it will retrieve the new rows, which results in phantom reads. Because shared locks are held to the end 
of a transaction instead of being released at the end of each statement, concurrency is lower than 
the default READ COMMITTED isolation level. Use this option only when necessary. 
*/