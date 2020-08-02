using AutoMapper;
using Backend.OpenWeathermap.Service;
using Backend.Weatherforecast.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BackendTest.Weatherforecast
{
    public class AutoMappingTest
    {
        //[Fact]
        public void TestForecatMapping()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<Main, Backend.Weatherforecast.Service.Weather>()
                .ForMember(x => x.FeelsLikeTemperature, o => o.MapFrom(y => y.feels_like))
                .ForMember(x => x.Humidity, o => o.MapFrom(y => y.humidity))
                .ForMember(x => x.MaximumTemperature, o => o.MapFrom(y => y.temp_max))
                .ForMember(x => x.MinimumTemperature, o => o.MapFrom(y => y.temp_min));

                c.CreateMap<Wind, Backend.Weatherforecast.Service.Weather>()
                   .ForMember(x => x.WindDirection, o => o.MapFrom(y => y.deg))
                   .ForMember(x => x.Windspeed, o => o.MapFrom(y => y.speed));

                c.CreateMap<Backend.OpenWeathermap.Service.Weather, Backend.Weatherforecast.Service.Weather>()
                   .ForMember(x => x.CloudDescription, o => o.MapFrom(y => y.description));

                c.CreateMap<CurrentWeatherModel, CurrentWeather>()
                    .ForMember(x => x.DateTime, o => o.MapFrom(y => DateTimeUTC.FromSecondsSinceUnixEpoch(y.dt)));

                c.CreateMap<WeatherList, ForecastWeather>()
                    .ForMember(x => x.DateTime, o => o.MapFrom(y => DateTimeUTC.FromSecondsSinceUnixEpoch(y.dt)));

            });

            var mapper = new Mapper(mapperConfiguration);

           
            //IMapper mapper = new MapperConfiguration(c =>
            //    c.AddProfile<MappingProfile>()).CreateMapper();

            var result = mapper.Map<WeatherforecastModel>(TestUtilities.GetOpenWeathermapForcastJson());

            Assert.NotNull(mapper);
        }
    }
}
