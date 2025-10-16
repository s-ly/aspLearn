using var db = new AppDbContext();

Console.WriteLine("🚀 Начинаем работу с EF Core!");

AddUser("Сергей4", "sergey4@test.com", 35);
SetOrderForUser("Сергей4");


// Читаем пользователей
var users = db.Users.ToList();
Console.WriteLine("\n📋 Все пользователи:");
foreach (var u in users)
{
    Console.WriteLine($"ID: {u.Id}, Имя: {u.Name}, Email: {u.Email}, Возраст: {u.Age}");
}

// Обновляем существующего пользователя если нужно
// UpdateUserAge(1, 28);

// Добавляем заказ пользователю
void SetOrderForUser(string nameUser)
{
    // находим пользователя по имени    
    var user = db.Users.First(u => u.Name == nameUser);

    // Создаём заказ
    var order = new Order
    {
        ProductName = "Книга",
        Price = 500,
        UserId = user.Id
    };

    db.Orders.Add(order);
    db.SaveChanges();
    Console.WriteLine("✅ Заказ добавлен!");
}

// Добавляем пользователя
void AddUser(string name, string email, int? age = null)
{
    var user = new User(name, email, age);
    db.Users.Add(user);
    db.SaveChanges();
    Console.WriteLine($"✅ Пользователь {name} добавлен!");
}

// Обновляет возраст существующего пользователя
void UpdateUserAge(int userId, int age)
{
    var user = db.Users.Find(userId);
    if (user != null)
    {
        user.Age = age;
        db.SaveChanges();
        Console.WriteLine($"✅ Возраст пользователя {user.Name} обновлён!");
    }

}