using Newtonsoft.Json;
using RestSharp;
using System.IO;

namespace APITesting
{
    public class APIHelper<DTO>
    {
        public RestClient restClient;
        public RestRequest restRequest;
        public string baseUrl = "http://91.210.252.240:9010/";

        public RestClient SetUrl(string endpoint)
        {
            var url = Path.Combine(baseUrl, endpoint);
            //var restClient = new RestClient(url);
            restClient = new RestClient(url);
            return restClient;
        }

        public RestRequest CreateGetRequest(string name = "", string value = "")
        {
            var restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            if (name != "" && value != "")
            {
                restRequest.AddParameter(name, value, ParameterType.QueryString);
            }
            var fullUrl = restClient.BuildUri(restRequest);
            return restRequest;
        }

        public RestRequest CreatePostRequest(string payload)
        {
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", payload, ParameterType.RequestBody);
            return restRequest;
        }

        public IRestResponse GetResponse(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }

        public DTO GetContent(IRestResponse response)
        {
            var content = response.Content;
            DTO dtoObject = JsonConvert.DeserializeObject<DTO>(content);
            return dtoObject;
        }

        public string Serialize(dynamic content)
        {
            string serializedObject = JsonConvert.SerializeObject(content, Formatting.Indented);
            return serializedObject;
        }
    }
}
