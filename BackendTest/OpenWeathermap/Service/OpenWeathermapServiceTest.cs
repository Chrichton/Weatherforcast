using Backend.OpenWeathermap.Service;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace BackendTest.OpenWeathermap.Service
{
    public class OpenWeathermapServiceTest
    {
        [Fact]
        public async void TestMockGetWeatherforecast()
        {
            var messageHandler = new MockHttpMessageHandler(File.ReadAllText(GetJsonPath()));
            using (var httpClient = new HttpClient(messageHandler))
            {
                var result = await new OpenWeathermapService(httpClient).GetWeatherforecast("Hamburg");
                Assert.NotNull(result);
                Assert.Equal("Hamburg", result.city.name); // Just shows, that the httpClient has been called
                Assert.Equal("2911298", messageHandler.idValue); // Shows that the service created a request with the correct id
            }
        }

        [Fact]
        public async void TestLiveGetWeatherforecast()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await new OpenWeathermapService(httpClient).GetWeatherforecast("Hamburg");
                Assert.NotNull(result);
                Assert.Equal("Hamburg", result.city.name);
                Assert.Equal(2911298, result.city.id);
            }
        }

        [Fact]
        public void TestDeserializeJSON()
        {
            var rootObject = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(GetJsonPath()));
            Assert.NotNull(rootObject);
        }

        private string GetJsonPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                @"OpenWeathermap\service\currentweather.json");
        }
    }

    internal class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string content;

        public string idValue { get; set; }
        public MockHttpMessageHandler(string content) => this.content = content;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var dictionary = HttpUtility.ParseQueryString(request.RequestUri.Query);
            idValue = dictionary["id"];

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content)
            };
        }
    }
}