using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace DiscussionPublisher.TelegramBot
{
    public class DiscussionPublisherTelegramBotService : ITelegramBotService
    {
        private ITelegramBotClient _botClient;

        public void StartBot()
        {
            var botConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _botClient = new TelegramBotClient(botConfig["TelegramBotSettings:ApiKey"]);
        }

        public async Task SendMessageToChannelAsync(string channelName, string message)
        {
            await _botClient.SendTextMessageAsync(channelName, message);
        }

        public async Task HandleUpdateAsync(Update update)
        {
            //todo Реализуй здесь логику обработки сообщений от пользователей
        }
    }
}