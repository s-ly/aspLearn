-- Таблица "заказы"
CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    person_id INTEGER, -- Ссылаемся на id из таблицы people
    product VARCHAR(100),
    price DECIMAL(10, 2), -- DECIMAL(10,2) = число с 2 знаками после запятой
    FOREIGN KEY (person_id) REFERENCES people (id) -- Связываем таблицы
);