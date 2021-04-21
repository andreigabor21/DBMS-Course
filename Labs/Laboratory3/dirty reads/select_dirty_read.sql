--Dirty read (unsolved)

--T2: select + delay + select
--we should see the update in the first select
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
BEGIN TRAN
	SELECT *
	FROM Client
	WAITFOR DELAY '00:00:15'
	SELECT *
	FROM Client	
COMMIT TRAN


--Dirty read (solved)
--T2: select + delay + select
--Here we do not see the update (that is rollbacked) – T1 finish first
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRAN
	SELECT *
	FROM Client
	WAITFOR DELAY '00:00:15'
	SELECT *
	FROM Client	
COMMIT TRAN