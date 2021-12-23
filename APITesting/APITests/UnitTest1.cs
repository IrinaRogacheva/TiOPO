using APITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace APITests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void VerifyListOfProducts()
        {
            var api = new API();
            var products = api.GetProducts("api/products");
            Assert.AreEqual(products[0].id, 2);
            Assert.AreEqual(products[0].alias, "casio-mq-24-7bul");
        }

        //[DeploymentItem("TestData/TestCase.json")]
        [TestMethod]
        public void CreateProduct()
        {
            var api = new API();
            var payload = HandleContent.ParseJson<CreateProductDTO>("TestData\\TestCase.json");
            var response = api.CreateProduct("api/addproduct", payload);
            var content = HandleContent.GetContent<CreateProductDTO>(response);
            //api.CreateProduct("api/addproduct", payload);
            //var product = api.GetProducts("api/products")[api.GetProducts("api/products").Count - 1];

            //Assert.AreEqual(payload.price, content.price);
        }
    }
}
