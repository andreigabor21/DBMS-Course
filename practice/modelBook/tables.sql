CREATE DATABASE modelBook

USE modelBook

CREATE TABLE libraries(
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(30),
	location VARCHAR(40)
)

CREATE TABLE books(
	id INT PRIMARY KEY IDENTITY,
	library_id INT REFERENCES libraries(id),
	title VARCHAR(30),
	author_id INT REFERENCES authors(id),
	category VARCHAR(30),
	publisher_id INT REFERENCES publishers(id)
)

CREATE TABLE authors(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(30)
)

CREATE TABLE publishers(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(30)
)

CREATE TABLE readers(
	id INT PRIMARY KEY IDENTITY,
	name VARCHAR(30),
	preference VARCHAR(30),
	affiliation_id INT REFERENCES affiliations(id)
)

CREATE TABLE affiliations(
	id INT PRIMARY KEY IDENTITY
)

CREATE TABLE reader_book(
	reader_id INT REFERENCES readers(id),
	book_id INT REFERENCES books(id),
	PRIMARY KEY(reader_id, book_id)
)

CREATE TABLE author_book(
	author_id INT REFERENCES authors(id),
	book_id INT REFERENCES books(id),
	PRIMARY KEY(author_id, book_id)
)

INSERT INTO publishers VALUES ('Andrei'), ('Matei');
INSERT INTO libraries VALUES('lib', 'asd')
INSERT INTO authors VALUES ('author')
INSERT INTO publishers VALUES('pub')
INSERT INTO books values(1, 'title', 1, 'science', 1)

SELECT * FROM books


