using DiscussionPublisherAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionPublisherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackageInfoController
    {
        [HttpPost]
        public IActionResult CreatePackageInfo([FromBody] PackageInfo questionSet)
        {
            if (string.IsNullOrWhiteSpace(questionSet.Header) || string.IsNullOrWhiteSpace(questionSet.Description))
            {
                return new BadRequestObjectResult("Header and Description cannot be empty.");
            }

            // todo Telegram-bot

            return new OkObjectResult("Package Info successfully created.");
        }
    }
}
