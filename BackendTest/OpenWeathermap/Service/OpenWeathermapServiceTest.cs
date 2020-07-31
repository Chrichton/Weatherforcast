using Backend.OpenWeathermap.Service;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BackendTest.OpenWeathermap.Service
{
    public class OpenWeathermapServiceTest
    {
        [Fact]
        public async void TestMockGetWeatherforecast()
        {
            using (var httpClient = new HttpClient(new MockHttpMessageHandler(File.ReadAllText(GetJsonPath()))))
            {
                var result = await new OpenWeathermapService(httpClient).GetWeatherforecast("Hamburg");
                Assert.NotNull(result);
                Assert.Equal("Hamburg", result.city.name);
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

        public MockHttpMessageHandler(string content) => this.content = content;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content)
            };
        }
    }
}