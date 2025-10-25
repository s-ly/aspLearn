using Microsoft.EntityFrameworkCore;
using WebSchool.Data;
using WebSchool.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Используем PostgreSQL
// string connectionStr = "Host=localhost;Database=gentrifr;Username=gentrifr;Port=5432"; // в школе
string connectionStr = "Host=localhost;Database=mydb;Username=sergey;Password=2988";  // в домашнем
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionStr));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// посмотреть всех пользователей
var actGetUsers = async (AppDbContext db) =>
{
    var users = await db.WebUsers.ToListAsync();
    return Results.Ok(users);
};

// получить пользователя по ID
var actGetUserById = async (int id, AppDbContext db) =>
{
    var user = await db.WebUsers.FindAsync(id);
    return user is not null ? Results.Ok(user) : Results.NotFound($"Пользователь с Id {id} не найден.");
};

// добавить пользователя
var actSetUser = async (WebUser newUser, AppDbContext db) =>
{
    // await db.WebUsers.AddAsync(new WebUser("Sergey"));
    await db.WebUsers.AddAsync(newUser);
    await db.SaveChangesAsync();
    return Results.Ok();
};

// Обновить пользователя по ID.
var actUpdateUser = async (int id, WebUser updateUser, AppDbContext db) =>
{
    var existingUser = await db.WebUsers.FindAsync(id);
    if (existingUser is null)
        return Results.NotFound($"Пользователь с ID {id} не найден.");
    existingUser.Name = updateUser.Name;
    await db.SaveChangesAsync();
    return Results.Ok(existingUser);
};

var actDeleteUser = async (int id, AppDbContext db) =>
{
    var user = await db.WebUsers.FindAsync(id);
    if (user is null)
        return Results.NotFound($"Пользователь с ID {id} не найден");
    db.WebUsers.Remove(user);
    await db.SaveChangesAsync();
    return Results.Ok($"Пользователь с ID {id} удалён.");
};

app.MapGet("/", () => "Hello World!");
app.MapGet("/getusers", actGetUsers);
app.MapGet("/getuser/{id}", actGetUserById);
app.MapPost("/setuser", actSetUser);
app.MapPut("/updateuser/{id}", actUpdateUser);
app.MapDelete("/deleteuser/{id}", actDeleteUser);

app.Run();