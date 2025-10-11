-- Только имена
SELECT name FROM people;

-- Люди старше 25 лет
SELECT * FROM people WHERE age > 25;

-- Заказы дороже 1000 рублей
SELECT * FROM orders WHERE price > 1000;

-- Сумма всех заказов
SELECT SUM(price) FROM orders;

-- Количество заказов у каждого человека
SELECT people.name, COUNT(orders.id) 
FROM people 
LEFT JOIN orders ON people.id = orders.person_id
GROUP BY people.name;