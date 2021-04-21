--T1: update + delay + rollback
BEGIN TRAN
	UPDATE Client 
	SET Email = 'new_email@tld.ro'
	WHERE SSN = '5000987123501'
	WAITFOR DELAY '00:00:10'
ROLLBACK TRAN

