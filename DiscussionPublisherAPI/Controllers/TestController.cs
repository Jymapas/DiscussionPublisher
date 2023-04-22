using DiscussionPublisher.TelegramBot;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DiscussionPublisherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITelegramBotService _telegramBotService;
        private readonly ILogger<TestController> _logger;

        public TestController(ITelegramBotService telegramBotService, ILogger<TestController> logger)
        {
            _telegramBotService = telegramBotService;
            _logger = logger;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            Console.WriteLine("Test");
            Log.Information("Test");
            await _telegramBotService.SendMessageToChannelAsync("Test"); ;
            return Ok("Test");
        }
    }
}
