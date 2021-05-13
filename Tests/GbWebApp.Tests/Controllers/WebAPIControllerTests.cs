using Moq;
using System;
using System.Net;
using GbWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GbWebApp.Interfaces.TestAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Assert = Xunit.Assert;

namespace GbWebApp.Tests.Controllers
{
    [TestClass]
    public class WebAPIControllerTests
    {
        private Mock<IValuesService> _valuesServiceMock;
        private WebAPIController _controller;

        private static readonly string[] ExpectedGetValues = { "1", "2", "3" };
        private const int TrueId = 1;
        private const int FakeId = 0;

        private class ValSrv : IValuesService   // not good way!
        {
            public IEnumerable<string> Get() => ExpectedGetValues;
            public string Get(int id) => throw new NotImplementedException();
            public Uri Create(string value) => throw new NotImplementedException();
            public HttpStatusCode Edit(int id, string value) => throw new NotImplementedException();
            public bool Remove(int id) => throw new NotImplementedException();
        }

        [TestInitialize]
        public void MockInit()
        {
            _valuesServiceMock = new Mock<IValuesService>();
            _valuesServiceMock.Setup(s => s.Get()).Returns(ExpectedGetValues);
            _valuesServiceMock.Setup(s => s.Get(It.IsAny<int>())).Returns<int>(id => id > 0 ? "NonEmpty" : string.Empty);
            //_valuesServiceMock.Setup(s => s.Get(FakeId)).Returns(string.Empty);
            //_valuesServiceMock.Setup(s => s.Get(TrueId)).Returns("NonEmpty");

            _controller = new WebAPIController(_valuesServiceMock.Object);
        }

        [TestMethod]
        public void Index_Returns_View_unit(/*[FromServices] IValuesService valuesService // not allowed and non sense */)
        {
            var valuesService = new ValSrv();
            var controller = new WebAPIController(valuesService);
            var result = controller.Index();

            var model = Assert.IsType<ViewResult>(result);
            Assert.Equal(valuesService.Get(), model.Model);
        }

        [TestMethod]
        public void Index_Returns_View_mock()
        {
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.Model);
            Assert.Equal(ExpectedGetValues, model);

            _valuesServiceMock.Verify(service => service.Get());
            _valuesServiceMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void DeleteValue_Returns_Redirect()
        {
            var result = _controller.DeleteValue(TrueId);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(WebAPIController.Index), redirectResult.ActionName);

            _valuesServiceMock.Verify(srv => srv.Get(TrueId));
            _valuesServiceMock.Verify(srv => srv.Remove(TrueId));
            _valuesServiceMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void DeleteValue_Returns_NotFound()
        {
            var result = _controller.DeleteValue(FakeId);
            var redirectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, redirectResult.StatusCode);

            _valuesServiceMock.Verify(srv => srv.Get(FakeId));
            _valuesServiceMock.VerifyNoOtherCalls();
        }
    }
}
