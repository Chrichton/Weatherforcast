using AutoMapper;
using Backend.OpenWeathermap.Service;
using static Backend.Weatherforecast.Service.DateTimeUTC;

namespace Backend.Weatherforecast.Service
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Main, Weather>()
                .ForMember(x => x.FeelsLikeTemperature, o => o.MapFrom(y => y.feels_like))
                .ForMember(x => x.Humidity, o => o.MapFrom(y => y.humidity))
                .ForMember(x => x.MaximumTemperature, o => o.MapFrom(y => y.temp_max))
                .ForMember(x => x.MinimumTemperature, o => o.MapFrom(y => y.temp_min));

            CreateMap<Wind, Weather>()
               .ForMember(x => x.WindDirection, o => o.MapFrom(y => y.deg))
               .ForMember(x => x.Windspeed, o => o.MapFrom(y => y.speed));

            CreateMap<Backend.OpenWeathermap.Service.Weather, Weather>()
               .ForMember(x => x.CloudDescription, o => o.MapFrom(y => y.description));

            CreateMap<CurrentWeatherModel, CurrentWeather>()
                .ForMember(x => x.DateTime, o => o.MapFrom(y => FromSecondsSinceUnixEpoch(y.dt)));

            CreateMap<WeatherList, ForecastWeather>()
                .ForMember(x => x.DateTime, o => o.MapFrom(y => FromSecondsSinceUnixEpoch(y.dt)));
        }
    }
}
