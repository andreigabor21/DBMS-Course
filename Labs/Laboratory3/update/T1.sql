USE BookLibrary;

--in order to replicate the update conflict
--we need to allow snapshots
ALTER DATABASE BookLibrary SET ALLOW_SNAPSHOT_ISOLATION ON

--first transaction
WAITFOR DELAY '00:00:10'
BEGIN TRAN
UPDATE Votes SET NumberOfStars=3 WHERE VoteID = 28
--NumberOfStars is now 3
WAITFOR DELAY '00:00:10'
COMMIT TRAN
 

SELECT * FROM Votes

ALTER DATABASE BookLibrary SET ALLOW_SNAPSHOT_ISOLATION OFF


--UNDO
UPDATE VOTES
SET NumberOfStars = 10
WHERE VoteID = 28