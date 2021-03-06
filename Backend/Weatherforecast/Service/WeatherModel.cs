﻿using System;
using System.Text.Json.Serialization;

namespace Backend.Weatherforecast.Service
{
    public class WeatherModel
    {
        public WeatherModel(Weather current, Weather[] forecast)
        {
            Current = current ?? throw new ArgumentNullException(nameof(current));
            Forecast = forecast ?? throw new ArgumentNullException(nameof(forecast));
        }

        [JsonPropertyName("AverageTemperature")]
        public float AverageTemperature { get; set; }

        [JsonPropertyName("AverageHumidity")]
        public float AverageHumidity { get; set; }

        public Weather Current { get; }

        public Weather[] Forecast { get; }
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

        [JsonPropertyName("Icon")]
        public string Icon { get; set; }
    }
}
