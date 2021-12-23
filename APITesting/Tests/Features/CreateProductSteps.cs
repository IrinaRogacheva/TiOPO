using System;
using TechTalk.SpecFlow;
using APITesting;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Features
{
    [Binding]
    public class CreateProductSteps
    {
        private readonly CreateProductDTO createProductDTO;
        private IRestResponse response;

        public CreateProductSteps(CreateProductDTO createProductDTO)
        {
            this.createProductDTO = createProductDTO;
        }

        [Given(@"id is (.*)")]
        public void GivenIdIs(int id)
        {
            createProductDTO.id = id;
        }
        
        [Given(@"category_id is (.*)")]
        public void GivenCategory_IdIs(int categoryId)
        {
            createProductDTO.category_id = categoryId;
        }
        
        [Given(@"title is ""(.*)""")]
        public void GivenTitleIs(string title)
        {
            createProductDTO.title = title;
        }
        
        [Given(@"alias is ""(.*)""")]
        public void GivenAliasIs(string alias)
        {
            createProductDTO.alias = alias;
        }
        
        [Given(@"content is ""(.*)""")]
        public void GivenContentIs(string content)
        {
            createProductDTO.content = content;
        }
        
        [Given(@"price is (.*)")]
        public void GivenPriceIs(int price)
        {
            createProductDTO.price = price;
        }
        
        [Given(@"old_price is (.*)")]
        public void GivenOld_PriceIs(int oldPrice)
        {
            createProductDTO.old_price = oldPrice;
        }
        
        [Given(@"status is (.*)")]
        public void GivenStatusIs(int status)
        {
            createProductDTO.status = status;
        }
        
        [Given(@"keywords is ""(.*)""")]
        public void GivenKeywordsIs(string keywords)
        {
            createProductDTO.keywords = keywords;
        }
        
        [Given(@"description is ""(.*)""")]
        public void GivenDescriptionIs(string description)
        {
            createProductDTO.description = description;
        }
        
        [Given(@"hit is (.*)")]
        public void GivenHitIs(int hit)
        {
            createProductDTO.hit = hit;
        }
        
        [When(@"send create product request")]
        public void WhenSendCreateProductRequest()
        {
            var api = new API();
            response = api.CreateProduct("api/addproduct", createProductDTO);
        }

        [Then(@"product is created")]
        public void ThenProductIsCreated()
        {
            var content = HandleContent.GetContent<CreateProductDTO>(response);
            var api = new API();
            ProductDTO addedProduct = api.GetProductById(content.id);
            Assert.IsNotNull(addedProduct, "This product shold have been added");

            Assert.AreEqual(content.id, addedProduct.id, "id is not equal to expected " + content.id);
            Assert.AreEqual(createProductDTO.category_id, addedProduct.category_id, "category_id is not equal to expected " + createProductDTO.category_id);
            Assert.AreEqual(createProductDTO.title, addedProduct.title, "title is not equal to expected " + createProductDTO.title);
            Assert.AreEqual(createProductDTO.content, addedProduct.content, "content is not equal to expected " + createProductDTO.content);
            Assert.AreEqual(createProductDTO.price, addedProduct.price, "price is not equal to expected " + createProductDTO.price);
            Assert.AreEqual(createProductDTO.old_price, addedProduct.old_price, "old_price is not equal to expected " + createProductDTO.old_price);
            Assert.AreEqual(createProductDTO.status, addedProduct.status, "status is not equal to expected " + createProductDTO.status);
            Assert.AreEqual(createProductDTO.keywords, addedProduct.keywords, "keywords is not equal to expected " + createProductDTO.keywords);
            Assert.AreEqual(createProductDTO.description, addedProduct.description, "description is not equal to expected " + createProductDTO.description);
            Assert.AreEqual(createProductDTO.hit, addedProduct.hit, "hit is not equal to expected " + createProductDTO.hit);

            api.DeleteProduct("api/deleteproduct", content.id);
        }

        [Then(@"check is alias equal to expected")]
        public void ThenCheckAlias()
        {
            var content = HandleContent.GetContent<CreateProductDTO>(response);
            var api = new API();
            ProductDTO addedProduct = api.GetProductById(content.id);
            Assert.IsNotNull(addedProduct, "This product shold have been added");

            Assert.AreEqual(createProductDTO.alias, addedProduct.alias, "alias is not equal to expected " + createProductDTO.alias);
            
            api.DeleteProduct("api/deleteproduct", content.id);
        }

        [Then(@"product is not created")]
        public void ThenProductIsNotCreated()
        {
            var content = HandleContent.GetContent<CreateProductDTO>(response);
            var api = new API();

            Assert.IsNull(api.GetProductById(content.id), "This product should not have been added");
        }
    }
}
