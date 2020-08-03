﻿using Backend.OpenWeathermap;
using Backend.OpenWeathermap.Service;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
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
            var cityToIntMapping = new Dictionary<string, int> { { "Hamburg", 2911298 } };
            var cityToIdProvider = new CityToIdProvider(cityToIntMapping);
            var messageHandler = new MockHttpMessageHandler(TestUtilities.GetOpenWeathermapForcastJson());
            using (var httpClient = new HttpClient(messageHandler))
            {
                var result = await new OpenWeathermapService(logger, httpClient, cityToIdProvider)
                    .GetWeatherforecast("Hamburg")
                    .ConfigureAwait(false);
                Assert.NotNull(result);
                Assert.Equal("Hamburg", result.city.name); // Just shows, that the httpClient has been called
                Assert.Equal("2911298", messageHandler.idValue); // Shows that the service created a request with the correct id
            }
        }

        [Fact, Description("UnkownCity")]
        public async void TestGetWeatherforecastUnkownCity()
        {
            var messageHandler = new MockHttpMessageHandler(TestUtilities.GetOpenWeathermapForcastJson());
            using (var httpClient = new HttpClient(messageHandler))
            {
                await Assert.ThrowsAsync<ArgumentException>("city", async () =>
                     await new OpenWeathermapService(logger, httpClient, new CityToIdProvider(new Dictionary<string, int>()))
                         .GetWeatherforecast("Hamburg").ConfigureAwait(false))
                    .ConfigureAwait(false);
            }
        }

        [Fact, Description("LiveTest")]
        public async void TestLiveGetWeatherforecast() // IntegrationTest should not be run on CI
        {
            using (var httpClient = new HttpClient())
            {
                var result = await new OpenWeathermapService(logger, httpClient, new CityToIdProvider())
                    .GetWeatherforecast("Hamburg")
                    .ConfigureAwait(false);
                Assert.NotNull(result);
                Assert.Equal("Hamburg", result.city.name);
                Assert.Equal(2911298, result.city.id);
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
            var cityToIntMapping = new Dictionary<string, int> { { "Hamburg", 2911298 } };
            var cityToIdProvider = new CityToIdProvider(cityToIntMapping);
            var messageHandler = new MockHttpMessageHandler(TestUtilities.GetOpenWeathermapCurrentWeatherJson());
            using (var httpClient = new HttpClient(messageHandler))
            {
                var result = await new OpenWeathermapService(logger, httpClient, cityToIdProvider)
                    .GetCurrentWeather("Hamburg")
                    .ConfigureAwait(false);
                Assert.NotNull(result);
                Assert.Equal("Hamburg", result.name); // Just shows, that the httpClient has been called
                Assert.Equal("2911298", messageHandler.idValue); // Shows that the service created a request with the correct id
            }
        }

        [Fact, Description("UnkownCity")]
        public async void TestGetCurrentWeatherUnkownCity()
        {
            var messageHandler = new MockHttpMessageHandler(TestUtilities.GetOpenWeathermapForcastJson());
            using (var httpClient = new HttpClient(messageHandler))
            {
                await Assert.ThrowsAsync<ArgumentException>("city", async () =>
                     await new OpenWeathermapService(logger, httpClient, new CityToIdProvider(new Dictionary<string, int>()))
                         .GetCurrentWeather("Hamburg").ConfigureAwait(false))
                    .ConfigureAwait(false); 
            }
        }

        [Fact, Description("LiveTest")]
        public async void TestLiveGetCurrentWeather() // IntegrationTest should not be run on CI
        {
            using (var httpClient = new HttpClient())
            {
                var result = await new OpenWeathermapService(logger, httpClient, new CityToIdProvider())
                    .GetCurrentWeather("Hamburg")
                    .ConfigureAwait(false); 
                Assert.NotNull(result);
                Assert.Equal("Hamburg", result.name);
                Assert.Equal(2911298, result.id);
            }
        }

        [Fact]
        public void TestDeserializeCurrentWeatherJSON()
        {
            var model = JsonSerializer.Deserialize<OpenWeatherMapCurrent>(TestUtilities.GetOpenWeathermapCurrentWeatherJson());
            Assert.NotNull(model);
        }

        #endregion

        private class MockHttpMessageHandler : HttpMessageHandler
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
}