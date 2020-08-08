using System.Collections.Generic;

namespace Backend.OpenWeathermap
{
    public interface ICitynameToIdProvider
    {
        IDictionary<string, int> GetDictionary();
    }
}