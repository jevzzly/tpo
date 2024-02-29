using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

namespace App3
{
    internal class Program
    {
        static HttpClient httpClient = new HttpClient();

        public static void Main(string[] args)
        {

           
                //   var httpClient = new HttpClient();
                var apiBaseUrl = "https://petstore.swagger.io/v2";
                var id = 555;
                var apiUrl = $"{apiBaseUrl}/pet/{id}";

          
                //  var response = await httpClient.GetAsync(apiUrl);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
                // выполняем запрос
                Task<HttpResponseMessage> response = httpClient.SendAsync(request);
                
                Console.WriteLine(response);
        }
    }
}