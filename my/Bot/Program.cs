using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

// для управления асинхронными операциями.
using var cts = new CancellationTokenSource();

var bot = new TelegramBotClient("1926895541:AAEzal5n00cdISLZckMuQNlxOf6Ak0yGt5g", cancellationToken: cts.Token);
var me = await bot.GetMe();

// Бот подписывается на событие OnMessage, которое вызывается при получении нового сообщения.
bot.OnMessage += OnMessage;

Console.WriteLine($"Привет, я пользователь {me.Id} и моё имя {me.FirstName}.");
Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel();

// Обработчик сообщений
async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text is null) return;
    Console.WriteLine($"Получен {type} '{msg.Text}' в {msg.Chat}");
    await bot.SendMessage(msg.Chat, $"{msg.From} сказал: {msg.Text}");
}