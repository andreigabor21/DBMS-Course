--in order to replicate the update conflict
--we need to allow snapshots
ALTER DATABASE BookLibrary SET ALLOW_SNAPSHOT_ISOLATION ON

--first transaction
WAITFOR DELAY '00:00:10'
BEGIN TRAN
UPDATE Votes SET NumberOfStars=3 WHERE VoteID = 28
--title name is now Champion
WAITFOR DELAY '00:00:10'
COMMIT TRAN
 

SELECT * FROM Votes

ALTER DATABASE BookLibrary SET ALLOW_SNAPSHOT_ISOLATION OFF

UPDATE VOTES
SET NumberOfStars = 10
WHERE VoteID = 28