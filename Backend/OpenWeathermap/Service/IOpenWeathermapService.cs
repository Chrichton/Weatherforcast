using System.Threading.Tasks;

namespace Backend.OpenWeathermap.Service
{
    public interface IOpenWeathermapService
    {
        /// <summary>
        /// Retrieves the data for the current weather
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>data for the current weather</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        /// <exception cref="ArgumentException">When city is unknown</exception>
        Task<OpenWeatherMapCurrent> GetCurrentWeather(string city);

        /// <summary>
        /// Retrieves the data for the weatherforecast
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>data for the weaterforecast</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        /// <exception cref="ArgumentException">When city is unknown</exception>
        Task<OpenWeathermapForecast> GetWeatherforecast(string city);
    }
}