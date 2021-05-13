using System;
using GbWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Assert = Xunit.Assert;

namespace GbWebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Throw_Raises_Exception()
        {
            var controller = new HomeController();
            controller.Throw();
        }
    }
}
