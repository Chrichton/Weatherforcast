using System.Linq;

namespace Backend.Weatherforecast.Service
{
    public static class WeatherModelExtensions
    {
        public static float CalculateAverageHumidity(this WeatherModel model)
        {
            return model.Forecast.Length == 0
                ? 0
                : (float)model.Forecast.Sum(c => c.Humidity) / model.Forecast.Length;
        }

        public static float CalculateAverageTemperature(this WeatherModel model)
        {
            return model.Forecast.Length == 0
                ? 0
                : model.Forecast.Sum(c => c.Temperature) / model.Forecast.Length;
        }
    }
}
