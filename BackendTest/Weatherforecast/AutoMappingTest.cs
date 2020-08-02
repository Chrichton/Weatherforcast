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

            CurrentWeatherModel currentModel = JsonSerializer.Deserialize<CurrentWeatherModel>(
                TestUtilities.GetOpenWeathermapCurrentWeatherJson());
            CurrentWeather current = mapper.Map<CurrentWeatherModel, CurrentWeather>(currentModel);
            Assert.NotNull(current);

            WeatherforecastModel forecastModel = JsonSerializer.Deserialize<WeatherforecastModel>(
                TestUtilities.GetOpenWeathermapForcastJson());
            ForecastWeather forecast = mapper.Map<WeatherforecastModel, ForecastWeather>(forecastModel);
            Assert.NotNull(forecast);
        }
    }
}
