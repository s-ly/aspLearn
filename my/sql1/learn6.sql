-- Показываем кто что заказал
SELECT people.name, orders.product, orders.price
FROM people
JOIN orders ON people.id = orders.person_id;