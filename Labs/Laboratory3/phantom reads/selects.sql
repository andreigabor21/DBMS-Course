USE BookLibrary;

--phantom reads (unsolved)

--we see the new value in the second select
--T2: select + delay + select
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
	SELECT * 
	FROM PublishingHouse
	WAITFOR DELAY '00:00:05'
	SELECT * 
	FROM PublishingHouse
COMMIT TRAN  


--phantom reads (solved)
--holds locks(including key-range locks) during the entire transaction
--locks existing data, as well as *data that doesn't exist*
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRAN
	SELECT * 
	FROM PublishingHouse
	WAITFOR DELAY '00:00:05'
	SELECT * 
	FROM PublishingHouse
COMMIT TRAN


/*
Range locks are placed in the range of key values that match the search conditions of
each statement executed in a transaction. This blocks other transactions from updating or inserting 
any rows that would qualify for any of the statements executed by the current transaction. 
This means that if any of the statements in a transaction are executed a second time, they will read 
the same set of rows. The range locks are held until the transaction completes. This is the most 
restrictive of the isolation levels because it locks entire ranges of keys and holds the locks 
until the transaction completes. Because concurrency is lower, use this option only when necessary. 
This option has the same effect as setting HOLDLOCK on all tables in all SELECT statements in a transaction.
*/