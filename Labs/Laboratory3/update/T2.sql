-- T2 update conflict
SET TRANSACTION ISOLATION LEVEL SNAPSHOT 

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