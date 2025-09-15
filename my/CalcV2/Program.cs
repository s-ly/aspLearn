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
    // a.Arg = new Arguments();
    // a.Res = new Resultat();

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

Func<Resultat> act_calculate = () =>
{
    int res = arguments.X + arguments.Y;
    result.Result = res;
    addus();
    logus();
    return result;
};

var act_SetArg = (Arguments arg) =>
{
    arguments.X = arg.X;
    arguments.Y = arg.Y;
    return Results.Ok();
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

