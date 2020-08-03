using AutoMapper;
using Backend.OpenWeathermap.Service;
using Backend.Weatherforecast.Common;

namespace Backend.Weatherforecast.Service
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Current
            CreateMap<OpenWeatherMapCurrent, Weather>(MemberList.Destination)
                .ForMember(x => x.DateTime, o =>
                    o.MapFrom(y => DateTimeUTC.FromSecondsSinceUnixEpoch(y.dt)))
                .ForMember(x => x.CloudDescription, o => o.MapFrom(y => y.weather[0].description))
                .ForMember(x => x.FeelsLikeTemperature, o => o.MapFrom(y => y.main.feels_like))
                .ForMember(x => x.Humidity, o => o.MapFrom(y => y.main.humidity))
                .ForMember(x => x.MaximumTemperature, o => o.MapFrom(y => y.main.temp_max))
                .ForMember(x => x.MinimumTemperature, o => o.MapFrom(y => y.main.temp_min))
                .ForMember(x => x.Pressure, o => o.MapFrom(y => y.main.pressure))
                .ForMember(x => x.Temperature, o => o.MapFrom(y => y.main.temp))
                .ForMember(x => x.WindDirection, o => o.MapFrom(y => y.wind.deg))
                .ForMember(x => x.Windspeed, o => o.MapFrom(y => y.wind.speed));

            // Forecast
            CreateMap<WeatherList, Weather>(MemberList.Destination)
                .ForMember(x => x.DateTime, o =>
                    o.MapFrom(y => DateTimeUTC.FromSecondsSinceUnixEpoch(y.dt)))
                .ForMember(x => x.CloudDescription, o => o.MapFrom(y => y.weather[0].description))
                .ForMember(x => x.FeelsLikeTemperature, o => o.MapFrom(y => y.main.feels_like))
                .ForMember(x => x.Humidity, o => o.MapFrom(y => y.main.humidity))
                .ForMember(x => x.MaximumTemperature, o => o.MapFrom(y => y.main.temp_max))
                .ForMember(x => x.MinimumTemperature, o => o.MapFrom(y => y.main.temp_min))
                .ForMember(x => x.Pressure, o => o.MapFrom(y => y.main.pressure))
                .ForMember(x => x.Temperature, o => o.MapFrom(y => y.main.temp))
                .ForMember(x => x.WindDirection, o => o.MapFrom(y => y.wind.deg))
                .ForMember(x => x.Windspeed, o => o.MapFrom(y => y.wind.speed));
        }
    }
}