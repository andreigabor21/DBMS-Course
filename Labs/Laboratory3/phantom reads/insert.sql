USE BookLibrary;

--phantom reads
--Transaction T1 reads a set of rows based on a search predicate
--Transaction T2 generates a new row that matches the search predicate while T1 is ongoing
--If T1 issues the same read operation, it will get an extra row

--T1: delay + insert + commit
BEGIN TRAN
WAITFOR DELAY '00:00:05'
INSERT INTO PublishingHouse
VALUES (6, 'Editura', 'Cluj, Str.Dorobanti', 'editura@tld.ro', '07837392', 'www.editura.ro');
COMMIT TRAN


SELECT * FROM PublishingHouse;

--UNDO
DELETE FROM 
PublishingHouse WHERE Id = 6;