using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using TestWebAPI;
using System.Net.Http;
using System.Web.Http;
using DataAccess;

using Common;


namespace UnitTeststingTrial
{
    [TestFixture]
    public class TestProductController
    {
        private ProductController _ProductController;
        [SetUp]
        public void a()
        {
            var b = 1;
            var t = b;
        }
        [Test]
        public void AddTest()
        {
            // Arrange
            ITrans trans=new Transaction();
            var ProductTRansactionPerformer = new GemericCRUD<Product>(trans);
            var controller = new ProductController(ProductTRansactionPerformer);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Get(10);

            // Assert
            Product product;

            //Assert.IsTrue(response.TryGetContentValue<Product>(out product));
            Assert.AreEqual(10, response.ProductID);

            //_ProductController.Request = CreateRequestMessage();
            //Assert.AreEqual(30, 30);
        }

        public HttpRequestMessage CreateRequestMessage(HttpMethod method = null, string uriString = null)
        {
            method = method ?? HttpMethod.Get;
            var uri = string.IsNullOrWhiteSpace(uriString)
            ? new Uri("http://localhost:39402/api/product/80 ")
            : new Uri(uriString);
            var requestMessage = new HttpRequestMessage(method, uri);
            requestMessage.SetConfiguration(new HttpConfiguration());
            return requestMessage;
        }

    }
}
