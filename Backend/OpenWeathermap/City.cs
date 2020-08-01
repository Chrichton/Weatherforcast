
using System.Text.Json.Serialization;

namespace Backend.OpenWeathermap
{
    /// <summary>
    /// City from city.list.json
    /// </summary>
    public class City
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("coord")]
        public Coordinate Coordinate { get; set; }
    }
}

