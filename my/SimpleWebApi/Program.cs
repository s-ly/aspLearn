using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Data;
using SimpleWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настраиваем конвейер запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 1. Тестовый endpoint
app.MapGet("/", () => "Hello World! API is working! 🚀");

// 2. Получить всех пользователей
app.MapGet("/users", async (AppDbContext db) =>
{
    var users = await db.Users.ToListAsync();
    return Results.Ok(users);
});

// 3. Создать нового пользователя (УПРОЩЁННАЯ ВЕРСИЯ)
app.MapPost("/users", async (User newUser, AppDbContext db) =>
{
    // Просто добавляем пользователя как есть
    db.Users.Add(newUser);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{newUser.Id}", newUser);
});

// 4. Получить пользователя по ID
app.MapGet("/users/{id}", async (int id, AppDbContext db) =>
{
    var user = await db.Users.FindAsync(id);
    return user != null ? Results.Ok(user) : Results.NotFound();
});

app.Run();