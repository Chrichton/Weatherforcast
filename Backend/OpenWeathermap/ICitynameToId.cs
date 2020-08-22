using System.Collections.Generic;

namespace Backend.OpenWeathermap
{
    public interface ICitynameToId
    {
        Dictionary<string, int> Dictionary { get; }
    }
}