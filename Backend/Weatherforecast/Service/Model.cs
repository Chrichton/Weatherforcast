using System;
using System.Text.Json.Serialization;

namespace Backend.Weatherforecast.Service
{
    public class Model
    {
        [JsonPropertyName("AverageTemperature")]
        public double AverageTemperature { get; set; }

        [JsonPropertyName("AverageHumidity")]
        public int AverageHumidity { get; set; }

        Weather Current { get; set; }

        Weather[] Forecast { get; set; }
    }

    public class Weather
    {
        [JsonPropertyName("FeelsLikeTemperature")]
        public double FeelsLikeTemperature { get; set; }

        [JsonPropertyName("MinimumTemperature")]
        public double MinimumTemperature { get; set; }

        [JsonPropertyName("MaximumTemperature")]
        public double MaximumTemperature { get; set; }

        [JsonPropertyName("Humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("Windspeed")]
        public int Windspeed { get; set; }

        [JsonPropertyName("WindDirection")]
        public int WindDirection { get; set; }

        [JsonPropertyName("CloudDescription")]
        public string CloudDescription { get; set; }

        [JsonPropertyName("DateTime")]
        public DateTime DateTime { get; set; }
    }
}
