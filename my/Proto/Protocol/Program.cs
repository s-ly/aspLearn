using System.Collections.Concurrent;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var _fruit = new ConcurrentDictionary<string, Fruit>();

app.MapGet("/", () => "Welcome!");

// Создает группу маршрутов, вызывая MapGroup и предоставляя префикс.
RouteGroupBuilder fruitApiGroup = app.MapGroup("/fruit");

// Конечные точки, определенные в группе маршрутов, будут иметь префикс группы, добавленный к маршруту.
fruitApiGroup.MapGet("/", () => _fruit);

var GetFruit = (string id) =>
    _fruit.TryGetValue(id, out var fruit)
    ? TypedResults.Ok(fruit)
    : Results.Problem(statusCode: 404);
fruitApiGroup.MapGet("/{id}", GetFruit);

fruitApiGroup.MapPost("/{id}", (Fruit fruit, string id) => _fruit.TryAdd(id, fruit));

app.Run();

record Fruit(string Name, int Stock);