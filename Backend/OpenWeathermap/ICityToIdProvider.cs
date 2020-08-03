using System.Collections.Generic;

namespace Backend.OpenWeathermap
{
    public interface ICityToIdProvider
    {
        IDictionary<string, int> GetDictionary();
    }
}