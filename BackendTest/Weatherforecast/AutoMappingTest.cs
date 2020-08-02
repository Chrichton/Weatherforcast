using AutoMapper;
using Backend.OpenWeathermap.Service;
using Backend.Weatherforecast.Service;
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

            ForecastWeather result = mapper.Map<ForecastWeather>(TestUtilities.GetOpenWeathermapForcastJson());
            Assert.NotNull(result);
        }
    }
}
