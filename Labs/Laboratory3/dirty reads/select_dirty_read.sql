USE BookLibrary;
--Dirty read (unsolved)
--A transaction reads uncommitted data
--READ UNCOMMITTED: no shared locks when reading data

--T2: select + delay + select
--the dirty read will happen because we can read uncommitted changes
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
BEGIN TRAN
	SELECT * --we should see the update
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

--READ COMMITTED: a transaction can read data that has been previously read(but not modified 
--by another ongoing transaction)
--shared locks are released as soon as the SELECT operation is performed