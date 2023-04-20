using DiscussionPublisher.TelegramBot;
using DiscussionPublisherAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DiscussionPublisherAPI.Controllers.Tests
{
    [TestClass]
    public class PackageInfoControllerTests
    {
        [TestMethod]
        public async Task CreatePackageInfo_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var telegramBotService = new Mock<ITelegramBotService>();
            var logger = new NullLogger<PackageInfoController>();
            var controller = new PackageInfoController(telegramBotService.Object, logger);
            var packageInfo = new PackageInfo("", "");

            // Act
            var result = await controller.CreatePackageInfo(packageInfo);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            telegramBotService.Verify(s => s.SendMessageToChannelAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task CreatePackageInfo_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var telegramBotServiceMock = new Mock<ITelegramBotService>();
            var logger = new NullLogger<PackageInfoController>();
            var controller = new PackageInfoController(telegramBotServiceMock.Object, logger);
            var packageInfo = new PackageInfo("Test Header", "Test Description");

            // Act
            var result = await controller.CreatePackageInfo(packageInfo);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            telegramBotServiceMock.Verify(s => s.SendMessageToChannelAsync(It.IsAny<string>()), Times.Once);

        }
    }
}