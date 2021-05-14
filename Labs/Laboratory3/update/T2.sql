USE BookLibrary;

-- T2 update conflict
SET TRANSACTION ISOLATION LEVEL SNAPSHOT 

--An update conflict happens during the commit, as the database engine checks the rows in the
--version store against the committed rows and finds a mismatch.

BEGIN TRAN
SELECT * FROM Votes WHERE VoteID = 10
-- here the value old value is returned because T1 has not yet reached the update because of delay
WAITFOR DELAY '00:00:10'
SELECT * FROM Votes WHERE VoteID = 10
-- now when trying to update the same resource that T1 has updated and locked
UPDATE Votes SET NumberOfStars = 5 WHERE VoteID = 28
-- process will block
COMMIT TRAN


SELECT * FROM Votes

ALTER DATABASE BookLibrary SET ALLOW_SNAPSHOT_ISOLATION OFF
/*
When trying to commit the version of the row in the second update, SQL Server noticed that the 
first transaction was also trying to update the same row. Since there is no way for the database 
engine to know who should win (that is actually a business decision) it had to kill one transaction.
*/