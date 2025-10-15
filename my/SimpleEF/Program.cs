using var db = new AppDbContext();

Console.WriteLine("🚀 Начинаем работу с EF Core!");

AddUser("Сергей3", "sergey3@test.com", 25);

// Читаем пользователей
var users = db.Users.ToList();
Console.WriteLine("\n📋 Все пользователи:");
foreach (var u in users)
{
    Console.WriteLine($"ID: {u.Id}, Имя: {u.Name}, Email: {u.Email}, Возраст: {u.Age}");
}

// Обновляем существующего пользователя если нужно
// UpdateUserAge(1, 28);

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