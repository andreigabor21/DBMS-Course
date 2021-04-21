--deadlock
--T2: update on table Votes + delay + update on table PublishingHouse

BEGIN TRAN
	UPDATE Votes
	SET NumberOfStars = 5
	WHERE DateOfVote = '2021-04-06'
	WAITFOR DELAY '00:00:05'
	UPDATE PublishingHouse
	SET Email = 'T2@tld.com'
	WHERE PName = 'Curtea Veche'
COMMIT TRAN


SELECT * FROM PublishingHouse;
SELECT * FROM Votes;


-- solution : choose T1 as the victim by increasing T2's priority
SET DEADLOCK_PRIORITY HIGH
BEGIN TRAN
	UPDATE Votes
	SET NumberOfStars = 5
	WHERE DateOfVote = '2021-04-06'
	WAITFOR DELAY '00:00:05'
	UPDATE PublishingHouse
	SET Email = 'T2@tld.com'
	WHERE PName = 'Curtea Veche'
COMMIT TRAN