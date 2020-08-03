using System.Collections.Generic;

namespace Backend.OpenWeathermap.Service
{
    public interface ICityToIdProvider
    {
        IDictionary<string, int> GetDictionary();
    }
}