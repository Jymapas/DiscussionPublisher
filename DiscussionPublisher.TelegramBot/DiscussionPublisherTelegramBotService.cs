using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DiscussionPublisher.TelegramBot
{
    public class DiscussionPublisherTelegramBotService : ITelegramBotService
    {
        private readonly ILogger<DiscussionPublisherTelegramBotService> _logger;
        private ITelegramBotClient? _botClient;

        public DiscussionPublisherTelegramBotService(ILogger<DiscussionPublisherTelegramBotService> logger)
        {
            _logger = logger;
        }

        public void StartBot()
        {
            _botClient = new TelegramBotClient(Keys.BotKey);

            _logger.LogDebug("TelegramBotClient created");
        }

        public async Task SendMessageToChannelAsync( string message)
        {
            if (_botClient == null)
            {
                throw new NullReferenceException("_botClient is null. You must call StartBot before calling this method.");
            }

            await _botClient.SendTextMessageAsync(Keys.ChannelId, message);
            _logger.LogInformation("Message \"{message}\" sent to channel {channelId}", message, Keys.ChannelId);
        }

        public async Task HandleUpdateAsync(Update update)
        {
            //todo  Реализуй здесь логику обработки сообщений от пользователей и логирование этого в инфо
        }
    }
}