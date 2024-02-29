using System;
using System.Diagnostics;
using System.Net;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using FluentAssertions;
using RestSharp;


namespace App3
{
    public class TestClass
    {
        static HttpClient httpClient = new HttpClient();

        [Fact]
        public async Task ResponseCheck_ShouldReturn200AndOK()
        {
           
            var apiBaseUrl = "https://petstore.swagger.io/v2";
            var id = 555;
            var apiUrl = $"{apiBaseUrl}/pet/{id}";

          
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            HttpResponseMessage response = await httpClient.SendAsync(request);
           
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OK", response.ReasonPhrase);
        }

        [Fact]
        public async Task ResponseTime_ShouldBeLessThan400ms()
        {
            

            var apiBaseUrl = "https://petstore.swagger.io/v2";
            var id = 555;
            var apiUrl = $"{apiBaseUrl}/pet/{id}";

           
            var stopwatch = Stopwatch.StartNew();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            HttpResponseMessage response = await httpClient.SendAsync(request);
            stopwatch.Stop();

            
            var responseTime = stopwatch.ElapsedMilliseconds;
            Assert.True(responseTime < 400, $"Response time exceeded: {responseTime}ms");
        }

        [Fact]
        public async Task ResponseCheck_ShouldReturnValidJsonStructure()
        {
            

            var apiBaseUrl = "https://petstore.swagger.io/v2";
            var id = 555;
            var apiUrl = $"{apiBaseUrl}/pet/{id}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            HttpResponseMessage response = await httpClient.SendAsync(request);
            
            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<JObject>(responseBody);

            
            Assert.NotNull(jsonObject["id"]);
            Assert.NotNull(jsonObject["category"]);
            Assert.NotNull(jsonObject["name"]);
            Assert.NotNull(jsonObject["photoUrls"]);
            Assert.NotNull(jsonObject["tags"]);
            Assert.NotNull(jsonObject["status"]);

            Assert.IsType<long>(jsonObject["id"].Value<long>());
            Assert.IsType<JObject>(jsonObject["category"]);
            Assert.IsType<string>(jsonObject["name"].Value<string>());
            Assert.IsType<JArray>(jsonObject["photoUrls"]);
            Assert.IsType<JArray>(jsonObject["tags"]);
            Assert.IsType<string>(jsonObject["status"].Value<string>());

            Assert.NotNull(jsonObject["category"]["id"]);
            Assert.NotNull(jsonObject["category"]["name"]);

            Assert.IsType<long>(jsonObject["category"]["id"].Value<long>());
            Assert.IsType<string>(jsonObject["category"]["name"].Value<string>());
        }
    }
}
