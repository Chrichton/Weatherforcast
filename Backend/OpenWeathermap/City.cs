using Newtonsoft.Json;
namespace Backend.OpenWeathermap
{
    /// <summary>
    /// City from city.list.json
    /// </summary>
    public class City
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "coord")]
        public Coordinate Coordinate { get; set; }
    }
}

