--Unrepeatable reads (unsolved)

--T2: select + delay + select
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
--only the final result in both selects
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
	SELECT *
	FROM PublishingHouse
	WAITFOR DELAY '00:00:05'
	SELECT *
	FROM PublishingHouse
COMMIT TRAN