using AutoMapper;
using Backend.OpenWeathermap.Service;
using Backend.Weatherforecast.Service;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Linq;
using Xunit;

namespace BackendTest.Weatherforecast.Service
{
    public class WeatherServiceTest
    {
        private ILogger<WeatherService> logger = Substitute.For<ILogger<WeatherService>>();
        private IMapper mapper = Substitute.For<IMapper>();
        private IOpenWeathermapService openWeathermapService = Substitute.For<IOpenWeathermapService>();

        [Fact]
        public async void TestGetWeather()
        {
            IWeatherService service = new WeatherService(logger, mapper, openWeathermapService);
            Model result = await service.GetWeather("Hamburg").ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact]
        public async void TestGetWeatherNoCity()
        {
            await Assert.ThrowsAsync<ArgumentException>("city", async () =>
                await new WeatherService(logger, mapper, openWeathermapService)
                    .GetWeather("Heiko").ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        [Fact]
        public async void TestGetCityByZipCode()
        {
            IWeatherService service = new WeatherService(logger, mapper, openWeathermapService);
            var cities = await service.GetCitiesForZipCode(21037);

            Assert.Single(cities);
            Assert.Equal("Hamburg", cities.Single());
        }

        [Fact]
        public async void TestGetCityByZipCodeNoCity()
        {
            IWeatherService service = new WeatherService(logger, mapper, openWeathermapService);
            var cities = await service.GetCitiesForZipCode(2103);

            Assert.Equal(new string[] {}, cities);
        }
    }
}
