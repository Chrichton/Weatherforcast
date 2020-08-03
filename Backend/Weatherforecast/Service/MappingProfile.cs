using AutoMapper;
using Backend.OpenWeathermap.Service;
using System;

namespace Backend.Weatherforecast.Service
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Main, Backend.Weatherforecast.Service.Weather>(MemberList.Source)
                .ForMember(x => x.Temperature, o => o.MapFrom(y => y.temp))
                .ForMember(x => x.FeelsLikeTemperature, o => o.MapFrom(y => y.feels_like))
                .ForMember(x => x.MaximumTemperature, o => o.MapFrom(y => y.temp_max))
                .ForMember(x => x.MinimumTemperature, o => o.MapFrom(y => y.temp_min))
                .ForMember(x => x.Humidity, o => o.MapFrom(y => y.humidity))
                .ForMember(x => x.Pressure, o => o.MapFrom(y => y.pressure));

            CreateMap<Wind, Backend.Weatherforecast.Service.Weather>(MemberList.Source)
               .ForMember(x => x.WindDirection, o => o.MapFrom(y => y.deg))
               .ForMember(x => x.Windspeed, o => o.MapFrom(y => y.speed));

            CreateMap<Backend.OpenWeathermap.Service.Weather, Backend.Weatherforecast.Service.Weather>(MemberList.Source)
               .ForMember(x => x.CloudDescription, o => o.MapFrom(y => y.description))
               .ForSourceMember(s => s.id, o => o.DoNotValidate())
               .ForSourceMember(s => s.main, o => o.DoNotValidate())
               .ForSourceMember(s => s.icon, o => o.DoNotValidate());

            var map1 = CreateMap<OpenWeatherMapCurrent, CurrentWeather>()
                .AfterMap((curModel, currWeather) => currWeather.DateTime = DateTime.Now); // TODO for debugging
            map1.ForAllMembers(opt => opt.Ignore());
            map1.ForMember(x => x.DateTime, o => o.MapFrom(y => DateTimeUTC.FromSecondsSinceUnixEpoch(y.dt)));

            CreateMap<OpenWeathermapForecast, ForecastWeather>()
                .ForAllMembers(opt => opt.Ignore());

            var map2 = CreateMap<WeatherList, ForecastWeather>()
                .AfterMap((curModel, currWeather) => currWeather.DateTime = DateTime.Now); // TODO for debugging
            map2.ForAllMembers(opt => opt.Ignore());
            map2.ForMember(x => x.DateTime, o => o.MapFrom(y => DateTimeUTC.FromSecondsSinceUnixEpoch(y.dt)));
        }
    }
}