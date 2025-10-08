using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(opts => opts.LoggingFields = HttpLoggingFields.RequestProperties); // логирование
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

Resultat result = new Resultat();
Arguments arguments = new Arguments();
List<ActionList> allOperations = new List<ActionList>(); // список всех операций

// Наполняет список операций и результатов.
void addus()
{
    ActionList a = new ActionList();

    a.Arg.X = arguments.X;
    a.Arg.Y = arguments.Y;
    a.Res.Result = result.Result;

    allOperations.Add(a);
}

// ПсевдоЛогирование.
void logus()
{
    Console.WriteLine("------ list ------");
    foreach (ActionList a in allOperations)
    {
        Console.WriteLine($"X= {a.Arg.X} Y= {a.Arg.Y} RES= {a.Res.Result}");
    }
}

void logusId(int id)
{
    Console.WriteLine("------ one action ------");
    var data = allOperations[id];
    Console.WriteLine($"X= {data.Arg.X} Y= {data.Arg.Y} RES= {data.Res.Result}");
}

Func<IResult> act_calculate = () =>
{
    int res = arguments.X + arguments.Y;
    result.Result = res;
    addus();
    logus();
    // return  result;
    return Results.Ok(result);
};

var act_SetArg = (Arguments arg) =>
{
    arguments.X = arg.X;
    arguments.Y = arg.Y;
    return Results.Ok();
};

var act_oneResult = (int id) =>
{
    Console.WriteLine($"id= {id}");

    if (allOperations.Count == 0)
    {
        return Results.Problem("Список операций пуст");
    }
    if (id < 0 || id >= allOperations.Count)
    {
        return Results.Problem($"Неверный индекс: {id}");
    }

    logusId(id);
    return Results.Ok(allOperations[id]); // Возвращаем корректный результат
};

// только для демонстрации испотльзования собственных типов с привязкой данных
var act_oneResultParse = (ResultId id) =>
{
    Console.WriteLine($"id= {id}");

    if (allOperations.Count == 0)
    {
        return Results.Problem("Список операций пуст");
    }
    if (id.Id < 0 || id.Id >= allOperations.Count)
    {
        return Results.Problem($"Неверный индекс: {id}");
    }

    logusId(id.Id);
    return Results.Ok(); // Возвращаем корректный результат
};

app.UseHttpLogging(); // логирование

// обработчик ошибок
app.UseExceptionHandler("/error");

// Определим маршрут для ошибок
app.Map("/error", () => Results.Problem("Произошла ошибка. Попробуйте позже."));

// маршрут с ошибкой
app.MapGet("/force-error", () =>
{
    throw new InvalidOperationException("Принудительная ошибка");
}).WithSummary("маршрут с ошибкой");

app.MapGet("/", () => "Hello Calc!")
// .WithTags("Мой тег")
.WithSummary("Привествие")
.WithDescription("Привествие Hello Calc!");

// МАРШРУТ - Вычислить результат
app.MapGet("/calculate", act_calculate)
.WithSummary("Вычислить результат")
.WithDescription("Результат вычисления X и Y. Возвращает поле result");

// МАРШРУТ - Получить все результаты
app.MapGet("/results", () =>
{
    return Results.Ok(allOperations);
})
.WithSummary("Получить все результаты")
.WithDescription("получить все операции и результаты. Возвращает поле allOperations");

app.MapGet("/arguments", () => arguments)
.WithSummary("Получить аргументы X и Y")
.WithDescription("Возвращает поле arguments, которое содержит X и Y.");

app.MapGet("/oneResult/{id:int}", act_oneResult)
.WithSummary("GET /oneResult/{id} - получить результат по id")
.WithDescription("GET /oneResult/{id} - получить результат по id");

app.MapGet("/oneResultParse/{id}", act_oneResultParse)
.WithSummary("GET /oneResultParse/{id} - получить результат по id (TryParse())")
.WithDescription("GET /oneResultParse/{id} - получить результат по id (TryParse())");

app.MapPost("setArg", act_SetArg)
.WithSummary("POST /setArg - задать аргументы")
.WithDescription("POST /setArg - задать аргументы");

app.Run();

/*
Реализация TryParse в собственном типе, позволяющая выполнить разбор значений маршрута.
Например поступает строка p123, а он её нормально привязывает как int, так-как мы реализовали
TryParse(), а в ём 'p' - отделяется и остаток парсится как число.
Это даёт нам привязку данных в конечной точке.
*/
readonly record struct ResultId(int Id)
{
    public static bool TryParse(string? s, out ResultId result)
    {
        if (s is not null && s.StartsWith('p') && int.TryParse(s.AsSpan().Slice(1), out int id))
        {
            result = new ResultId(id);
            return true;
        }
        result = default;
        return false;
    }
}

// Первое и второе числа.
class Arguments
{
    public int X { get; set; }
    public int Y { get; set; }
}

// Результат вичислений.
class Resultat
{
    public int Result { get; set; }
}

// Аргументы и результат их вычисдения.
class ActionList
{
    public Arguments Arg { get; set; } = new Arguments();
    public Resultat Res { get; set; } = new Resultat();
}

