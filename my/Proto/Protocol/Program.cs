using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var _fruit = new ConcurrentDictionary<string, Fruit>();

app.MapGet("/", () => "Welcome!");
app.MapGet("/fruit", () => _fruit);

// Обработчик GET-запроса для получения конкретного фрукта (/fruit/{id})
// Логика:
// 1. Пытается получить фрукт по ID из словаря
// 2. Если найден - возвращает HTTP 200 с данными
// 3. Если не найден - возвращает HTTP 404 ProblemDetails
var GetFruit = (string id) =>
    _fruit.TryGetValue(id, out var fruit)
    ? TypedResults.Ok(fruit)
    : Results.Problem(statusCode: 404);

// Регистрируем маршрут и применяем валидацию ID через фильтр
// AddEndpointFilter - позволяет добавить middleware для проверки параметров
app.MapGet("/fruit/{id}", GetFruit).AddEndpointFilter(ValidationHelper.ValidateId);

var AddFruit = (string id, Fruit fruit) => _fruit.TryAdd(id, fruit);
app.MapPost("/fruit/{id}", AddFruit);

app.Run();

// Класс-валидатор для проверки формата идентификатора
class ValidationHelper
{
    // Фильтр проверяет первый аргумент (ID) на соответствие требованиям
    internal static async ValueTask<object?> ValidateId(
        EndpointFilterInvocationContext context, // содержащий информацию о текущем вызове эндпоинта
        EndpointFilterDelegate next) // Делегат, представляющий следующий шаг в цепочке фильтров или саму логику эндпоинта.
    {
        // Получаем ID из контекста вызова    
        var id = context.GetArgument<string>(0);

        // Проверяем валидность ID:
        // 1. Не должен быть пустым
        // 2. Должен начинаться с символа 'f'
        if (string.IsNullOrEmpty(id) || !id.StartsWith('f'))
        {
            return Results.ValidationProblem(
                    new Dictionary<string, string[]> {
                        {"id", new[]{"Invalid format. Id must start with 'f'"}}
                    });
        }

        // Если валидация прошла успешно - продолжаем выполнение
        return await next(context);
    }
}

record Fruit(string Name, int Stock);