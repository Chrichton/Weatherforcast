﻿using AutoMapper;
using Backend.OpenWeathermap;
using Backend.OpenWeathermap.Service;
using Backend.Weatherforecast;
using Backend.Weatherforecast.Service;
using LanguageExt;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BackendTest.Weatherforecast.Service
{
    public class WeatherServiceTest
    {
        private ILogger<WeatherService> logger = Substitute.For<ILogger<WeatherService>>();
        private IMapper mapper = Substitute.For<IMapper>();
        private IOpenWeathermapService openWeathermapService = Substitute.For<IOpenWeathermapService>();
        private ICitynamesIds citynamesIds = Substitute.For<ICitynamesIds>();

        [Fact]
        public async void TestGetWeather()
        {
            var openForecast = Option<OpenWeathermapForecast>
                .Some(new OpenWeathermapForecast{ list = new WeatherList[] {} });

            openWeathermapService.GetCurrentWeather(Arg.Any<int>())
                .Returns(Task.FromResult(Option<OpenWeathermapCurrent>.Some(new OpenWeathermapCurrent())));

            openWeathermapService.GetWeatherforecast(Arg.Any<int>())
                .Returns(Task.FromResult(openForecast));

            mapper.Map<OpenWeathermapCurrent, Backend.Weatherforecast.Service.Weather>(Arg.Any<OpenWeathermapCurrent>())
                .Returns(new Backend.Weatherforecast.Service.Weather());
            
            mapper.Map<WeatherList[], Backend.Weatherforecast.Service.Weather[]>(Arg.Any<WeatherList[]>())
                .Returns(new Backend.Weatherforecast.Service.Weather[] 
                { new Backend.Weatherforecast.Service.Weather { Humidity = 42, Temperature = 13 } });
            
            IWeatherService service = new WeatherService(logger, mapper, openWeathermapService,
                TestUtilities.ZipcodeToCitiesProvider, citynamesIds);
            var resultOpt = await service.GetWeather(TestUtilities.CityIdHamburg).ConfigureAwait(false);

            resultOpt
                .Some(result =>
                {
                    Assert.NotNull(result);
                    Assert.Equal(42f, result.AverageHumidity);
                    Assert.Equal(13f, result.AverageTemperature);
                })
                .None(() => Assert.False(true, "Test Failed"));
        }

        [Fact]
        public async void TestGetWeatherNoCity()
        {
            openWeathermapService.GetCurrentWeather(Arg.Any<int>())
                .Returns(Task.FromResult(Option<OpenWeathermapCurrent>.None));

            var openForecast = Option<OpenWeathermapForecast>
                .Some(new OpenWeathermapForecast { list = new WeatherList[] { } });
            openWeathermapService.GetWeatherforecast(Arg.Any<int>())
                .Returns(Task.FromResult(openForecast));

            var weatherOpt = await new WeatherService(logger, mapper, openWeathermapService, 
                TestUtilities.ZipcodeToCitiesProvider, citynamesIds)
                .GetWeather(TestUtilities.CityIdHamburg).ConfigureAwait(false);

            Assert.Equal(Option<WeatherModel>.None, weatherOpt);
        }

        [Fact]
        public async void TestGetCitiesIdsForZipCode()
        {
            var kvHamburg = new KeyValuePair<string, int>("Hamburg", 11111);
            citynamesIds.GetCityNameIdForCity("Hamburg")
                .Returns(Option<KeyValuePair<string,int>>.Some(kvHamburg));
            var dictionary = new Dictionary<int, IEnumerable<string>> { { 21037, new[] { "Hamburg" } } };
            IWeatherService service = new WeatherService(logger, mapper, openWeathermapService,
                TestUtilities.ZipcodeToCitiesProvider, citynamesIds);
            var cities = await service.GetCitiesIdsForZipCode(21037);

            Assert.Single(cities);
            Assert.Equal("Hamburg", cities.Single().Key);
            Assert.Equal(11111, cities.Single().Value);
        }

        [Fact]
        public async void TestGetCitiesIdsForZipCodeNoCity()
        {
            var dictionary = new Dictionary<int, IEnumerable<string>>();
            IWeatherService service = new WeatherService(logger, mapper, openWeathermapService,
                TestUtilities.ZipcodeToCitiesProvider, citynamesIds);
            var citiesIds = await service.GetCitiesIdsForZipCode(21037);

            Assert.Equal(new KeyValuePair<string,int>[] {}, citiesIds);
        }
    }
}
