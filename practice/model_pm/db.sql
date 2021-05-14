CREATE DATABASE model_pmm

DROP DATABASE model_pm

USE model_pmm
USE AppDB

CREATE TABLE projects(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(30),
	team_lead_id INT REFERENCES developers(id),
	category_id INT REFERENCES categories(id)
)

CREATE TABLE categories(
	id INT PRIMARY KEY IDENTITY(1,1),
	descr VARCHAR(40)
)

CREATE TABLE developers(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(30),
	age INT,
	years_exp INT
)

CREATE TABLE developer_project(
	id INT PRIMARY KEY IDENTITY(1,1),
	project_id INT REFERENCES projects(id),
	developer_id INT REFERENCES developers(id),
	join_date DATE
)

CREATE TABLE features(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(40),
	estimation_hours INT,
	task_id INT REFERENCES tasks(id)
)

CREATE TABLE tasks(
	id INT PRIMARY KEY IDENTITY(1,1),
	description VARCHAR(40),
	due_date DATE,
	developer_id INT REFERENCES developers(id)
)

CREATE TABLE developer_task(
	id INT PRIMARY KEY IDENTITY(1,1),
	task_id INT REFERENCES tasks(id),
	developer_id INT REFERENCES developers(id),
)


DROP TABLE developers;
DROP TABLE tasks;
