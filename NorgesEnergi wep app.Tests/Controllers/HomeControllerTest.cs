using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorgesEnergi_wep_app;
using NorgesEnergi_wep_app.Controllers;

namespace NorgesEnergi_wep_app.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
