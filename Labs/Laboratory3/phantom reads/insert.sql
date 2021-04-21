--phantom reads

--T1: delay + insert + commit
BEGIN TRAN
WAITFOR DELAY '00:00:05'
INSERT INTO PublishingHouse
VALUES (6, 'Editura', 'Cluj, Str.Dorobanti', 'editura@tld.ro', '07837392', 'www.editura.ro');
COMMIT TRAN


SELECT * FROM PublishingHouse;

DELETE FROM 
PublishingHouse WHERE Id = 6;