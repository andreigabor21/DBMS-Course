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
--
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRAN
	SELECT * 
	FROM PublishingHouse
	WAITFOR DELAY '00:00:05'
	SELECT * 
	FROM PublishingHouse
COMMIT TRAN
