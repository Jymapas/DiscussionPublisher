using Telegram.Bot.Types;
using Telegram.Bot;

namespace DiscussionPublisher.TelegramBot
{
    public class DiscussionPublisherTelegramBotService : ITelegramBotService
    {
        private ITelegramBotClient _botClient;

        public void StartBot()
        {
            _botClient = new TelegramBotClient();
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