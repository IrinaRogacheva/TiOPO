using Newtonsoft.Json;
using RestSharp;
using System.IO;

namespace APITesting
{
    public class HandleContent
    {
        public static T GetContent<T>(IRestResponse response)
        {
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static T ParseJson<T>(string file)
        {
            T content = JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
            //return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
            return content;
        }
    }
}
