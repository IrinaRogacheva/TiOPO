using System;
using TechTalk.SpecFlow;
using APITesting;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Features
{
    [Binding]
    public class UpdateProductSteps
    {
        private API api = new API();
        private CreateProductDTO updateProductDTO;
        private IRestResponse response;

        public UpdateProductSteps(CreateProductDTO updateProductDTO)
        {
            this.updateProductDTO = updateProductDTO;
        }

        [Given(@"id to update is (.*)")]
        public void GivenIdIs(int id)
        {
            updateProductDTO.id = id;
            updateProductDTO = api.GetProductInfoToUpdateById(id);
        }

        [Given(@"category_id to update is (.*)")]
        public void GivenCategory_IdIs(int categoryId)
        {
            updateProductDTO.category_id = categoryId;
        }
        
        [Given(@"status to update is (.*)")]
        public void GivenStatusIs(int status)
        {
            updateProductDTO.status = status;
        }
        
        [Given(@"hit to update is (.*)")]
        public void GivenHitIs(int hit)
        {
            updateProductDTO.hit = hit;
        }

        [When(@"send update product request")]
        public void WhenSendUpdateProductRequest()
        {
            response = api.UpdateProduct("api/editproduct", updateProductDTO);
        }

        [Then(@"category_id is updated")]
        public void ThenCategoryIdIsUpdated()
        {
            Assert.AreEqual(updateProductDTO.category_id, api.GetProductInfoToUpdateById(updateProductDTO.id).category_id, "Field category_id in this product should have updated to " + updateProductDTO.category_id);
        }

        [Then(@"status is updated")]
        public void ThenStatusIsUpdated()
        {
            Assert.AreEqual(updateProductDTO.status, api.GetProductInfoToUpdateById(updateProductDTO.id).status, "Field status in this product should have updated to " + updateProductDTO.status);
        }

        [Then(@"hit is updated")]
        public void ThenHitIsUpdated()
        {
            Assert.AreEqual(updateProductDTO.hit, api.GetProductInfoToUpdateById(updateProductDTO.id).hit, "Field hit in this product should have updated to " + updateProductDTO.hit);
        }

        [Then(@"category_id is not updated")]
        public void ThenCategoryIdIsNotUpdated()
        {
            Assert.AreNotEqual(updateProductDTO.category_id, api.GetProductInfoToUpdateById(updateProductDTO.id).category_id, "Field category_id in this product should not have updated");
        }

        [Then(@"status is not updated")]
        public void ThenStatusIsNotUpdated()
        {
            Assert.AreNotEqual(updateProductDTO.status, api.GetProductInfoToUpdateById(updateProductDTO.id).status, "Field status in this product should not have updated");
        }

        [Then(@"hit is not updated")]
        public void ThenHitIsNotUpdated()
        {
            Assert.AreNotEqual(updateProductDTO.hit, api.GetProductInfoToUpdateById(updateProductDTO.id).hit, "Field hit in this product should not have updated");
        }
    }
}
