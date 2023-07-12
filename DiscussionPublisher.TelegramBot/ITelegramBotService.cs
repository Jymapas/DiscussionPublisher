using Telegram.Bot.Types;

namespace DiscussionPublisher.TelegramBot
{
    public interface ITelegramBotService
    {
        void StartBot();
        Task SendMessageToChannelAsync(string message);
        Task HandleUpdateAsync(Update update);
    }
}
