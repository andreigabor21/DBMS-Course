--Unrepeatable reads

--T1: delay + update + commit
BEGIN TRAN
	WAITFOR DELAY '00:00:05'
	UPDATE PublishingHouse
	SET PhoneNumber = '0712345678'
	WHERE PName = 'RAO'
COMMIT TRAN



UPDATE PublishingHouse
SET PhoneNumber = '0798765432'
WHERE PName = 'RAO'

SELECT * FROM PublishingHouse