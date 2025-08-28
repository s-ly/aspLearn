using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Регистрация службы логирования HTTP-трафика.
// Логируем всё: запросы и ответы.
builder.Services.AddHttpLogging(opts => {
    opts.LoggingFields =
        HttpLoggingFields.Request |         // Запросы (метод, путь, заголовки)
        HttpLoggingFields.Response |        // Ответы (статус, заголовки)
        HttpLoggingFields.RequestBody |     // Тело запроса (если нужно)
        HttpLoggingFields.ResponseBody;     // Тело ответа (важно!)
});

// Фильтр логирования: выводим информационные сообщения и выше
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

var app = builder.Build();

// Активация промежуточного ПО в Development
if (app.Environment.IsDevelopment()) {
    app.UseHttpLogging();
}

app.MapGet("/", () => new Patient("Tony"));

app.Run();

public record Patient(string name);