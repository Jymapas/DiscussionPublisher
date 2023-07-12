using DiscussionPublisher.TelegramBot;
using DiscussionPublisherAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiscussionPublisherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageInfoController : ControllerBase
    {
        private readonly ITelegramBotService _telegramBotService;
        private readonly ILogger<PackageInfoController> _logger;

        public PackageInfoController(ITelegramBotService telegramBotService, ILogger<PackageInfoController> logger)
        {
            _telegramBotService = telegramBotService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackageInfo([FromBody] PackageInfo packageInfo)
        {
            if (string.IsNullOrWhiteSpace(packageInfo.Header) || string.IsNullOrWhiteSpace(packageInfo.Description))
            {
                _logger.LogError("Invalid request: {message}", "Header and Description cannot be empty.");
                return new BadRequestObjectResult("Header and Description cannot be empty.");
            }

            var message = packageInfo.GetMessage();

            _logger.LogInformation("Received package info: {message}", message);

            await _telegramBotService.SendMessageToChannelAsync(message);

            _logger.LogInformation("Package Info successfully created.");
            return new OkObjectResult("Package Info successfully created.");
        }
    }
}
