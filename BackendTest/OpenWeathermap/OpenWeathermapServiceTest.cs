using Backend.OpenWeathermap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class OpenWeathermapServiceTest
    {
        [Fact]
        public async void TestGetWeatherforecast()
        {
            using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
            {
                var result = await new OpenWeathermapService(httpClient).GetWeatherforecast("Hamburg");
                Assert.True(result.Length > 0);
            }
        }
    }
}

public class MockHttpMessageHandler : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"OpenWeathermap\currentweather.json");
        string json = File.ReadAllText(path);

        return new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(json)
        };
    }
}