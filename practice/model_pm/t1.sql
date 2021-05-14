--T1
BEGIN TRAN
	UPDATE categories
	SET descr = 'desccc' WHERE id = 1
ROLLBACK TRAN


INSERT INTO categories VALUES ('desc');

UPDATE categories
	SET descr = 'desc' WHERE id = 1