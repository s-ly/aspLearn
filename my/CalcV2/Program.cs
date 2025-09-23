using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(opts => opts.LoggingFields = HttpLoggingFields.RequestProperties); // логирование
var app = builder.Build();

Resultat result = new Resultat();
Arguments arguments = new Arguments();
List<ActionList> allOperations = new List<ActionList>(); // список всех операций

void addus()
{
    ActionList a = new ActionList();

    a.Arg.X = arguments.X;
    a.Arg.Y = arguments.Y;
    a.Res.Result = result.Result;

    allOperations.Add(a);
}

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
});

app.MapGet("/", () => "Hello Calc!");
app.MapGet("/result", act_calculate);
app.MapGet("/arguments", () => arguments);
app.MapGet("/oneResult/{id:int}", act_oneResult);
app.MapPost("setArg", act_SetArg);

app.Run();

class Arguments
{
    public int X { get; set; }
    public int Y { get; set; }
}

// record Resultat(int Result);
class Resultat
{
    public int Result { get; set; }
}

class ActionList
{
    public Arguments Arg { get; set; } = new Arguments();
    public Resultat Res { get; set; } = new Resultat();
}

