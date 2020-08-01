using System.Text.Json.Serialization;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// Coordinate from city.list.json
    /// </summary>
    public class Coordinate
    {
        [JsonPropertyName("lon")]
        public float Longitude { get; set; }

        [JsonPropertyName("lat")]
        public float Latitude { get; set; }
    }
}
