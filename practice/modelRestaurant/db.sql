CREATE DATABASE model_restaurant

USE model_restaurant


CREATE TABLE ingrediente(
	id INT PRIMARY KEY IDENTITY(1, 1),
	nume VARCHAR(40)
)

CREATE TABLE reteta(
	id INT PRIMARY KEY IDENTITY(1, 1),
	greutate INT,
	volum INT
)

CREATE TABLE meniu(
	id INT PRIMARY KEY IDENTITY(1, 1),
	reteta_id INT REFERENCES reteta(id),
	ingredient_id INT REFERENCES ingrediente(id)
)

CREATE TABLE mese(
	id INT PRIMARY KEY IDENTITY(1, 1),
	locuri INT
)

CREATE TABLE bucatari(
	id INT PRIMARY KEY IDENTITY(1, 1),
	nume VARCHAR(50)
)

CREATE TABLE ospatari(
	id INT PRIMARY KEY IDENTITY(1, 1),
	nume VARCHAR(50)
)

CREATE TABLE comenzi(
	id INT PRIMARY KEY IDENTITY(1, 1),
	ospatar_id INT REFERENCES ospatari(id),
	bucatar_id INT REFERENCES bucatari(id),
	masa_id INT REFERENCES mese(id),
	reteta_id INT REFERENCES reteta(id)
)