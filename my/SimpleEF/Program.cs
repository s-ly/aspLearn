using var db = new AppDbContext();

Console.WriteLine("🚀 Начинаем работу с EF Core!");

// Добавляем пользователя
var user = new User("Сергей", "sergey@test.com");
db.Users.Add(user);
db.SaveChanges();
Console.WriteLine("✅ Пользователь добавлен!");

// Читаем пользователей
var users = db.Users.ToList();
Console.WriteLine("\n📋 Все пользователи:");
foreach (var u in users)
{
    Console.WriteLine($"ID: {u.Id}, Имя: {u.Name}, Email: {u.Email}");
}