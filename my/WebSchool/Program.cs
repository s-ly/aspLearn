using Microsoft.EntityFrameworkCore;
using WebSchool.Data;
using WebSchool.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Используем PostgreSQL
string connectionStr = "Host=localhost;Database=gentrifr;Username=gentrifr;Port=5432"; // в школе
// string connectionStr = "Host=localhost;Database=mydb;Username=sergey;Password=2988";  // в домашнем
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

/*
// получить пользователя по ID
var actGetUserById = async (int id, AppDbContext db) =>
{
    var user = await db.WebUsers.FindAsync(id);
    return user is not null ? Results.Ok(user) : Results.NotFound($"User with ID {id} not found");
};

app.MapGet("/getuser/{id}", actGetUserById);






Альтернативный вариант с Where (если нужно больше контроля)
Если хочешь использовать LINQ Where вместо FindAsync:

// получить пользователя по ID (альтернатива)
var actGetUserById = async (int id, AppDbContext db) =>
{
    var user = await db.WebUsers
        .Where(u => u.Id == id)
        .FirstOrDefaultAsync();
    
    return user is not null ? Results.Ok(user) : Results.NotFound($"User with ID {id} not found");
};

FindAsync(id) - ищет по первичному ключу, использует кэш EF
FirstOrDefaultAsync(u => u.Id == id) - выполняет SQL запрос, больше контроля








Обработка ошибок (улучшенная версия)
var actGetUserById = async (int id, AppDbContext db) =>
{
    try
    {
        if (id <= 0)
            return Results.BadRequest("ID must be greater than 0");
        
        var user = await db.WebUsers.FindAsync(id);
        
        return user is not null 
            ? Results.Ok(user) 
            : Results.NotFound(new { message = $"User with ID {id} not found", id });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error retrieving user: {ex.Message}");
    }
};




*/