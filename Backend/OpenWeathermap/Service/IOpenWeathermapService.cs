using LanguageExt;
using System.Threading.Tasks;

namespace Backend.OpenWeathermap.Service
{
    public interface IOpenWeathermapService
    {
        /// <summary>
        /// Retrieves the data for the current weather
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>Some(data) for the current weather. None, when the city is unknown</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        public Task<Option<OpenWeatherMapCurrent>> GetCurrentWeather(string city);

        /// <summary>
        /// Retrieves the data for the weatherforecast
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>Some(data) for the weatherforecast. None, when the city is unknown</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        public Task<Option<OpenWeathermapForecast>> GetWeatherforecast(string city);
    }
}