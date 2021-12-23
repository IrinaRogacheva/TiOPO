using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITesting
{
    public class API
    {
        public List<ProductDTO> GetProducts(string endpoint)
        {
            var products = new APIHelper<List<ProductDTO>>();
            var url = products.SetUrl(endpoint);
            var request = products.CreateGetRequest();
            var response = products.GetResponse(url, request);
            List<ProductDTO> content = products.GetContent(response);
            return content;
        }

        public ProductDTO GetProductById(long id)
        {
            List<ProductDTO> products = GetProducts("api/products");
            ProductDTO found = products.FirstOrDefault(obj => obj.GetId() == id);
            return found;
        }

        public CreateProductDTO GetProductInfoToUpdateById(long id)
        {
            List<ProductDTO> products = GetProducts("api/products");
            ProductDTO found = products.FirstOrDefault(obj => obj.GetId() == id);
            CreateProductDTO createProductDTO = new CreateProductDTO();
            if (found != null)
            {
                createProductDTO.id = found.id;
                createProductDTO.title = found.title;
                createProductDTO.category_id = found.category_id;
                createProductDTO.alias = found.alias;
                createProductDTO.content = found.content;
                createProductDTO.price = found.price;
                createProductDTO.old_price = found.old_price;
                createProductDTO.status = found.status;
                createProductDTO.keywords = found.keywords;
                createProductDTO.description = found.description;
                createProductDTO.hit = found.hit;
            }    
            return createProductDTO;
        }

        public IRestResponse CreateProduct(string endpoint, dynamic payload)
        {
            var product = new APIHelper<CreateProductDTO>();
            var url = product.SetUrl(endpoint);
            var jsonReq = product.Serialize(payload);
            var request = product.CreatePostRequest(jsonReq);
            var response = product.GetResponse(url, request);
            return response;
        }

        public IRestResponse DeleteProduct(string endpoint, dynamic payload)
        {
            var apiHelper = new APIHelper<List<ProductDTO>>();
            var url = apiHelper.SetUrl(endpoint);
            var request = apiHelper.CreateGetRequest("id", payload.ToString());
            var response = apiHelper.GetResponse(url, request);
            return response;
        }

        public IRestResponse UpdateProduct(string endpoint, dynamic payload)
        {
            var product = new APIHelper<CreateProductDTO>();
            var url = product.SetUrl(endpoint);
            var jsonReq = product.Serialize(payload);
            var request = product.CreatePostRequest(jsonReq);
            var response = product.GetResponse(url, request);
            return response;
        }
    }
}
