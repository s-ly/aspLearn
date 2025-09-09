var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Resultat result = new Resultat();
Arguments arguments = new Arguments();

Func<Resultat> calculate = () =>
{
    int res = arguments.X + arguments.Y;
    result.Result = res;
    return result;
};

app.MapGet("/", () => "Hello Calc!");
app.MapGet("/result", calculate);
app.MapGet("/arguments", () => arguments);
app.MapPost("getArg", (Arguments arg) =>
{
    arguments.X = arg.X;
    arguments.Y = arg.Y;
    return Results.Ok();
});

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

