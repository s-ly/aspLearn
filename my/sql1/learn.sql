CREATE TABLE people(
    id SERIAL PRIMARY KEY,  -- SERIAL = автоматически увеличивающийся номер
    name VARCHAR(50),       -- VARCHAR(50) = текст до 50 символов
    age INTEGER             -- INTEGER = целое число
);