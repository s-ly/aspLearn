using Microsoft.AspNetCore.HttpLogging;

//builder.Services.AddHttpLogging(opts => opts.LoggingFields = HttpLoggingFields.RequestProperties);


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(opts => opts.LoggingFields = HttpLoggingFields.RequestProperties); // логирование
var app = builder.Build();

Resultat result = new Resultat();
Arguments arguments = new Arguments();

Func<Resultat> calculate = () =>
{
    int res = arguments.X + arguments.Y;
    result.Result = res;
    return result;
};

var SetArg = (Arguments arg) =>
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
// app.Map("/error", () => "Произошла ошибка. Попробуйте позже!.");

// маршрут с ошибкой
app.MapGet("/force-error", () =>
{
    throw new InvalidOperationException("Принудительная ошибка");
});

app.MapGet("/", () => "Hello Calc!");
app.MapGet("/result", calculate);
app.MapGet("/arguments", () => arguments);
app.MapPost("setArg", SetArg);

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

