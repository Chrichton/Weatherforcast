using AutoMapper;
using Backend.OpenWeathermap.Service;
using System.Text.Json;
using Xunit;
using Weather = Backend.Weatherforecast.Service.Weather;

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
            Weather current = mapper.Map<OpenWeatherMapCurrent, Weather>(currentModel);
            Assert.NotNull(current);

            OpenWeathermapForecast forecastModel = JsonSerializer.Deserialize<OpenWeathermapForecast>(
                TestUtilities.GetOpenWeathermapForcastJson());
            Weather forecast = mapper.Map<OpenWeathermapForecast, Weather>(forecastModel);
            Assert.NotNull(forecast);
        }
    }
}
