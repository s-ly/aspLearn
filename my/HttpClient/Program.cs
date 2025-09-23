// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;

Console.WriteLine("Клиент для HTTP-калькулятора");
var client = new HttpClient();

while (true)
{
    Console.Write("\nВведите команду (0 - посмотреть команды): ");
    var input = Console.ReadLine();
    switch (input)
    {
        case "0": PrintComand(); break;
        case "1": await SendGetRequest("/"); break;
        case "2": await SendGetRequest("/arguments"); break;
        case "3": await SetArguments(); break;
        case "4": await SendGetRequest("/result"); break;
        case "5": await SetOneResult(); break;
        case "6": await SetOneResultParse(); break;
        case "9": return;
        default: Console.WriteLine("Неверная команда."); break;
    }

}

// Печатает справку по командам
static void PrintComand()
{
    Console.WriteLine("0 - посмотреть команды");
    Console.WriteLine("1 - GET / - привествие");
    Console.WriteLine("2 - GET /arguments - получить аргументы");
    Console.WriteLine("3 - POST /setArg - задать аргументы");
    Console.WriteLine("4 - GET /result - получить результат");
    Console.WriteLine("5 - GET /oneResult/{id} - получить результат по id");
    Console.WriteLine("6 - GET /oneResultParse/{id} - получить результат по id (TryParse())");
    Console.WriteLine("9 - выход");
}

// отправляет GET-запросы
async Task SendGetRequest(string endpoint)
{
    var response = await client.GetAsync($"http://localhost:5224{endpoint}");
    await PrintRequest(response);
}

async Task SetArguments()
{
    Console.Write("X: ");
    if (!int.TryParse(Console.ReadLine(), out int x))
    {
        Console.WriteLine("❌ Неверный формат X");
        return;
    }
    Console.Write("Y: ");
    if (!int.TryParse(Console.ReadLine(), out int y))
    {
        Console.WriteLine("❌ Неверный формат Y");
        return;
    }

    var args = new { X = x, Y = y };
    var response = await client.PostAsJsonAsync($"http://localhost:5224/setArg", args);
    await PrintRequest(response);
}

async Task SetOneResult()
{
    Console.Write("id: ");
    if (!int.TryParse(Console.ReadLine(), out int r))
    {
        Console.WriteLine("❌ Неверный формат id");
        return;
    }
    string finalEndPoint = "/oneResult/" + r.ToString();
    await SendGetRequest(finalEndPoint);
}

async Task SetOneResultParse()
{
    Console.Write("id: ");
    if (!int.TryParse(Console.ReadLine(), out int r))
    {
        Console.WriteLine("❌ Неверный формат id");
        return;
    }
    string finalEndPoint = "/oneResultParse/" + 'p' + r.ToString();
    await SendGetRequest(finalEndPoint);
}

// Печатает ответ
static async Task PrintRequest(HttpResponseMessage response)
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine("\n--- ОТВЕТ: -----------------------");
    Console.WriteLine($"Статус: {response.StatusCode}");
    Console.WriteLine($"Тело: {content}");
    Console.WriteLine($"-----------------------------------");
    // Console.WriteLine($"{response.StatusCode}");
}