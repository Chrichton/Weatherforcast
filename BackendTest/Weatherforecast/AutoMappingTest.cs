using AutoMapper;
using Backend.OpenWeathermap.Service;
using Backend.Weatherforecast.Service;
using System.Text.Json;
using Xunit;

namespace BackendTest.Weatherforecast
{
    public class AutoMappingTest
    {
        [Fact]
        public void TestMapping()
        {
            MapperConfiguration cfg = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Backend.Startup)));
            cfg.AssertConfigurationIsValid();

            IMapper mapper = cfg.CreateMapper();
            Assert.NotNull(mapper);

            OpenWeatherMapCurrent currentModel = JsonSerializer.Deserialize<OpenWeatherMapCurrent>(
                TestUtilities.GetOpenWeathermapCurrentWeatherJson());
            CurrentWeather current = mapper.Map<OpenWeatherMapCurrent, CurrentWeather>(currentModel);
            Assert.NotNull(current);

            OpenWeathermapForecast forecastModel = JsonSerializer.Deserialize<OpenWeathermapForecast>(
                TestUtilities.GetOpenWeathermapForcastJson());
            ForecastWeather forecast = mapper.Map<OpenWeathermapForecast, ForecastWeather>(forecastModel);
            Assert.NotNull(forecast);
        }
    }
}
