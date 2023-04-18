using DiscussionPublisherAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiscussionPublisherAPI.Controllers.Tests
{
    [TestClass]
    public class PackageInfoControllerTests
    {
        [TestMethod]
        public void CreatePackageInfo_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var controller = new PackageInfoController();
            var packageInfo = new PackageInfo { Header = "", Description = "" };

            // Act
            var result = controller.CreatePackageInfo(packageInfo);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void CreatePackageInfo_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var controller = new PackageInfoController();
            var packageInfo = new PackageInfo { Header = "Test Header", Description = "Test Description" };

            // Act
            var result = controller.CreatePackageInfo(packageInfo);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}