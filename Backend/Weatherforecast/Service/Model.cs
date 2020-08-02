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

        CurrentWeather Current { get; set; }

        ForecastWeather[] Forecast { get; set; }
    }

    public class Weather
    {
        [JsonPropertyName("Temperature")]
        public float Temperature { get; set; }

        [JsonPropertyName("FeelsLikeTemperature")]
        public float FeelsLikeTemperature { get; set; }

        [JsonPropertyName("MinimumTemperature")]
        public float MinimumTemperature { get; set; }

        [JsonPropertyName("MaximumTemperature")]
        public float MaximumTemperature { get; set; }

        [JsonPropertyName("Humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("Pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("Windspeed")]
        public float Windspeed { get; set; }

        [JsonPropertyName("WindDirection")]
        public int WindDirection { get; set; }

        [JsonPropertyName("CloudDescription")]
        public string CloudDescription { get; set; }

        [JsonPropertyName("DateTime")]
        public DateTime DateTime { get; set; }
    }

    public class CurrentWeather: Weather
    { }

    public class ForecastWeather: Weather
    { }
}
