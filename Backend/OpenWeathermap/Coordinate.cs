using Newtonsoft.Json;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// Coordinate from city.list.json
    /// </summary>
    public class Coordinate
    {
        [JsonProperty(PropertyName = "lon")]
        public float Longitude { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public float Latitude { get; set; }
    }
}
