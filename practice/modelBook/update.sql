--non-repeatable reads

BEGIN TRAN
	WAITFOR DELAY '00:00:05'
	UPDATE books
	SET category = 'new_cat'
	WHERE title = 'titlu'
COMMIT TRAN


--undo
UPDATE books
SET category = 'cat'
WHERE title = 'titlu'