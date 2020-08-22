using Backend.OpenWeathermap;
using Backend.OpenWeathermap.Service;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace BackendTest.OpenWeathermap.Service
{
    public class OpenWeathermapServiceTest
    {
        private ILogger<OpenWeathermapService> logger = Substitute.For<ILogger<OpenWeathermapService>>();

        #region Weatherforecast

        [Fact]
        public async void TestGetWeatherforecast()
        {
            var cityToIntMapping = new Dictionary<string, int> { { "Hamburg", TestUtilities.CityIdHamburg } };
            var messageHandler = new MockHttpMessageHandler(TestUtilities.GetOpenWeathermapForcastJson());
            using (var httpClient = new HttpClient(messageHandler))
            {
                var resultOpt = await new OpenWeathermapService(logger, httpClient, TestUtilities.CitynameToId)
                    .GetWeatherforecast(TestUtilities.CityIdHamburg)
                    .ConfigureAwait(false);

                resultOpt
                    .Some(result =>
                    {
                        Assert.NotNull(result);
                        Assert.Equal("Hamburg", result.city.name); // Just shows, that the httpClient has been called
                        Assert.Equal(TestUtilities.CityIdHamburg, messageHandler.idValue); // Shows that the service created a request with the correct id
                    })
                    .None(() => Assert.False(true, "Test Failed"));
            }
        }

        [Fact, Description("UnkownCity")]
        public async void TestGetWeatherforecastUnkownCity()
        {
            var messageHandler = new MockHttpMessageHandler(TestUtilities.GetOpenWeathermapForcastJson());
            using (var httpClient = new HttpClient(messageHandler))
            {
                var resultOpt = await new OpenWeathermapService(logger, httpClient, TestUtilities.CitynameToId)
                    .GetWeatherforecast(TestUtilities.CityIdHamburg)
                    .ConfigureAwait(false);

                resultOpt
                    .Some(_ => Assert.False(true, "Test Failed"));
            }
        }

        [Fact, Description("LiveTest")]
        public async void TestLiveGetWeatherforecast() // IntegrationTest should not be run on CI
        {
            using (var httpClient = new HttpClient())
            {
                var resultOpt = await new OpenWeathermapService(logger, httpClient, TestUtilities.CitynameToId)
                    .GetWeatherforecast(TestUtilities.CityIdHamburg)
                    .ConfigureAwait(false);

                resultOpt
                    .Some(result =>
                    {
                        Assert.NotNull(result);
                        Assert.Equal("Hamburg", result.city.name);
                        Assert.Equal(TestUtilities.CityIdHamburg, result.city.id);
                    })
                    .None(() => Assert.False(true, "Test Failed"));
            }
        }

        [Fact]
        public void TestDeserializeWeatherforecastJSON()
        {
            var model = JsonSerializer.Deserialize<OpenWeathermapForecast>(TestUtilities.GetOpenWeathermapForcastJson());
            Assert.NotNull(model);
        }

        #endregion

        #region CurrentWeather

        [Fact]
        public async void TestCurrentWeather()
        {
            var cityToIntMapping = new Dictionary<string, int> { { "Hamburg", TestUtilities.CityIdHamburg } };
            var messageHandler = new MockHttpMessageHandler(TestUtilities.GetOpenWeathermapCurrentWeatherJson());
            using (var httpClient = new HttpClient(messageHandler))
            {
                var resultOpt = await new OpenWeathermapService(logger, httpClient, TestUtilities.CitynameToId)
                    .GetCurrentWeather(TestUtilities.CityIdHamburg)
                    .ConfigureAwait(false);

                resultOpt
                    .Some(result =>
                    {
                        Assert.NotNull(result);
                        Assert.Equal("Hamburg", result.name); // Just shows, that the httpClient has been called
                        Assert.Equal(TestUtilities.CityIdHamburg, messageHandler.idValue); // Shows that the service created a request with the correct id
                    })
                    .None(() => Assert.False(true, "Test Failed"));
            }
        }

        [Fact, Description("UnkownCity")]
        public async void TestGetCurrentWeatherUnkownCity()
        {
            var messageHandler = new MockHttpMessageHandler(TestUtilities.GetOpenWeathermapCurrentWeatherJson());
            using (var httpClient = new HttpClient(messageHandler))
            {
              var resultOpt = await new OpenWeathermapService(logger, httpClient, TestUtilities.CitynameToId)
                    .GetCurrentWeather(TestUtilities.CityIdHamburg).ConfigureAwait(false);

                resultOpt
                    .Some(_ => Assert.False(true, "Test Failed"));
            }
        }

        [Fact, Description("LiveTest")]
        public async void TestLiveGetCurrentWeather() // IntegrationTest should not be run on CI
        {
            using (var httpClient = new HttpClient())
            {
                var resultOpt = await new OpenWeathermapService(logger, httpClient, TestUtilities.CitynameToId)
                    .GetCurrentWeather(TestUtilities.CityIdHamburg)
                    .ConfigureAwait(false);

                resultOpt
                    .Some(result =>
                    {
                        Assert.NotNull(result);
                        Assert.Equal("Hamburg", result.name);
                        Assert.Equal(TestUtilities.CityIdHamburg, result.id);                    
                    })
                    .None(() => Assert.False(true, "Test Failed"));
            }
        }

        [Fact]
        public void TestDeserializeCurrentWeatherJSON()
        {
            var model = JsonSerializer.Deserialize<OpenWeathermapCurrent>(TestUtilities.GetOpenWeathermapCurrentWeatherJson());
            Assert.NotNull(model);
        }

        #endregion

        private class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly string content;

            public int idValue { get; set; }
            public MockHttpMessageHandler(string content) => this.content = content;

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                var dictionary = HttpUtility.ParseQueryString(request.RequestUri.Query);
                idValue = int.Parse(dictionary["id"]);

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content)
                };
            }
        }
    }
}