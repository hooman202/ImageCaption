using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;


namespace ImageCaption
{
    static class Program
    {
        public class Discription
        {
            public List<string> tags { set; get; }
            //public List<string> captions { set; get; }
        }

        public class JsonResult
        {
            public Discription description { set; get; }
            
        }
        private static HttpResponseMessage _response;
        static void Main()
        {
            MakeRequest();
            Console.WriteLine("Hit ENTER to exit...");
            while (_response == null)
            {
            }
            var content = _response.Content.ReadAsStringAsync();
            content.Wait();
            Console.WriteLine("Done");
            var result = JsonConvert.DeserializeObject<JsonResult>(content.Result);
            Console.WriteLine(_response.Content.ToString());
            
        }
        
        static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

//             Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "a82edd914b7a49acb6bf2b423a86f289");
            
            // Request parameters
            queryString["visualFeatures"] = "Description";
//            queryString["details"] = "";
            queryString["language"] = "en";
            var uri = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?" + queryString;


            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{\"url\":\"http://www.rd.com/wp-content/uploads/sites/2/2016/02/06-train-cat-shake-hands.jpg\"}");


            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                _response =  await client.PostAsync(uri, content);
             


            }
            
        }
        

            /*
        public static async Task<HttpResponseMessage> RequestImageAnalisys(byte[] data)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            // Request headers		            
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{db3926cb19fe41269ec7361ede468b39}");
            // Request parameters		            
            queryString["visualFeatures"] = "Categories, Tags, Description, Faces, ImageType, Color, Adult";
            queryString["details"] = "Celebrities";
            queryString["language"] = "en";
            var uri = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?" + queryString;
            		
            HttpResponseMessage response;
            using (var content = new ByteArrayContent(data))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
            }
            return response;
        }
        */
    }
}