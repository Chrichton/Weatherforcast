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

            #region Current
            OpenWeatherMapCurrent currentModel = JsonSerializer.Deserialize<OpenWeatherMapCurrent>(
                TestUtilities.GetOpenWeathermapCurrentWeatherJson());
            Weather current = mapper.Map<OpenWeatherMapCurrent, Weather>(currentModel);

            Assert.NotNull(current);
            Assert.Equal("Klarer Himmel", current.CloudDescription);
            Assert.Equal(new System.DateTime(2020, 8, 1, 4, 6, 25), current.DateTime);
            Assert.Equal(14.36, current.FeelsLikeTemperature, 2);
            Assert.Equal(82, current.Humidity);
            Assert.Equal(17.22, current.MaximumTemperature, 2);
            Assert.Equal(15, current.MinimumTemperature, 2);
            Assert.Equal(1016, current.Pressure);
            Assert.Equal(15.71, current.Temperature, 2);
            Assert.Equal(110, current.WindDirection);
            Assert.Equal(3.1, current.Windspeed, 1);
            #endregion

            #region Forecast
            OpenWeathermapForecast forecastModel = JsonSerializer.Deserialize<OpenWeathermapForecast>(
                TestUtilities.GetOpenWeathermapForcastJson());
            Weather[] forecasts = mapper.Map<WeatherList[], Weather[]>(forecastModel.list);
            
            Assert.NotNull(forecasts);
            Assert.Equal(40, forecasts.Length);

            var forecast = forecasts[0];
            Assert.Equal("Bedeckt", forecast.CloudDescription);
            Assert.Equal(new System.DateTime(2020, 7, 30, 21, 0, 0), forecast.DateTime);
            Assert.Equal(15.46, forecast.FeelsLikeTemperature, 2);
            Assert.Equal(76, forecast.Humidity);
            Assert.Equal(15.87, forecast.MaximumTemperature, 2);
            Assert.Equal(13.58, forecast.MinimumTemperature, 2);
            Assert.Equal(1020, forecast.Pressure);
            Assert.Equal(15.87, forecast.Temperature, 2);
            Assert.Equal(272, forecast.WindDirection);
            Assert.Equal(1.32, forecast.Windspeed, 2);
            #endregion
        }
    }
}
