--deadlock
--T1: update on table PublishingHouse + delay + update on table Votes

BEGIN TRAN
	UPDATE PublishingHouse
	SET Email = 'T1@tld.com'
	WHERE PName = 'Curtea Veche'
	WAITFOR DELAY '00:00:05'
	UPDATE Votes
	SET NumberOfStars = 4
	WHERE DateOfVote = '2021-04-06'
COMMIT TRAN


SELECT * FROM PublishingHouse;
SELECT * FROM Votes;

--undo
UPDATE PublishingHouse
SET Email = 'curtea@veche.com'
WHERE PName = 'Curtea Veche'
UPDATE Votes
SET NumberOfStars = 1
WHERE DateOfVote = '2021-04-06'