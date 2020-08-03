using AutoMapper;
using Backend.OpenWeathermap.Service;

namespace Backend.Weatherforecast.Service
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Main, Weather>(MemberList.Source)
                .ForMember(x => x.Temperature, o => o.MapFrom(y => y.temp))
                .ForMember(x => x.FeelsLikeTemperature, o => o.MapFrom(y => y.feels_like))
                .ForMember(x => x.MaximumTemperature, o => o.MapFrom(y => y.temp_max))
                .ForMember(x => x.MinimumTemperature, o => o.MapFrom(y => y.temp_min))
                .ForMember(x => x.Humidity, o => o.MapFrom(y => y.humidity))
                .ForMember(x => x.Pressure, o => o.MapFrom(y => y.pressure));

            CreateMap<Wind, Weather>(MemberList.Source)
               .ForMember(x => x.WindDirection, o => o.MapFrom(y => y.deg))
               .ForMember(x => x.Windspeed, o => o.MapFrom(y => y.speed));

            CreateMap<Backend.OpenWeathermap.Service.Weather, Weather>(MemberList.Source)
               .ForMember(x => x.CloudDescription, o => o.MapFrom(y => y.description))
               .ForSourceMember(s => s.id, o => o.DoNotValidate())
               .ForSourceMember(s => s.main, o => o.DoNotValidate())
               .ForSourceMember(s => s.icon, o => o.DoNotValidate());



            CreateMap<OpenWeatherMapCurrent, Weather>(MemberList.None)
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
               

            CreateMap<OpenWeathermapForecast, Weather>(MemberList.None);

            CreateMap<WeatherList, Weather>(MemberList.None)
                .ForMember(x => x.DateTime, o => 
                    o.MapFrom(y => DateTimeUTC.FromSecondsSinceUnixEpoch(y.dt)));
        }
    }
}