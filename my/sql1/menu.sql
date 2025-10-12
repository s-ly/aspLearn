-- Создать таблицу
CREATE TABLE menu (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100)
);

-- Вставим строку
INSERT INTO menu (name) VALUES ('Лапша');

INSERT INTO menu (name) VALUES ('Суп солянка');

-- Удалить запись
DELETE FROM menu WHERE name = 'Лапша';

-- Посмотрим всю таблицу
SELECT * FROM menu;