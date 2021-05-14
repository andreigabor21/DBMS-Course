USE BookLibrary;

--T1: update + delay + rollback
--try to update and rollback after 10 seconds
BEGIN TRAN
	UPDATE Client 
	SET Email = 'new_email@tld.ro'
	WHERE SSN = '5000987123501'
	WAITFOR DELAY '00:00:10'
ROLLBACK TRAN

