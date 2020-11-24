using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string accessToken = "1486727353:AAF3yts1XVe3zKxJH-DLrhK0hIZbGuaeyNg";
            var bot = new TelegramBotClient(accessToken);
            bot.DeleteWebhookAsync().Wait();
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();
            Console.ReadKey();
            bot.StopReceiving();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var bot = sender as TelegramBotClient;
            await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Message received: {e.Message.Text}");
        }
    }
}
