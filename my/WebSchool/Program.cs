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

var actSetUser = async (WebUser newUser, AppDbContext db) =>
{
    // await db.WebUsers.AddAsync(new WebUser("Sergey"));
    await db.WebUsers.AddAsync(newUser);
    await db.SaveChangesAsync();
    return Results.Ok();
};

app.MapGet("/", () => "Hello World!");
app.MapGet("/getusers", actGetUsers);
app.MapPost("/setuser", actSetUser);


app.Run();
