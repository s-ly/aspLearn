using Microsoft.EntityFrameworkCore;
using WebSchool.Data;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Используем PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Host=localhost;Database=gentrifr;Username=gentrifr;Port=5432"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var act = () =>  "Users..."; 

app.MapGet("/", () => "Hello World!");
app.MapGet("/getusers", act);


app.Run();
