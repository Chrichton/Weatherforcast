using AutoMapper;
using Backend.OpenWeathermap.Service;

namespace Backend.Weatherforecast.Service
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CurrentWeatherModel, Model>();
                
        }
    }
}
