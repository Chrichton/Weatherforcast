using LanguageExt;
using System.Threading.Tasks;

namespace Backend.OpenWeathermap.Service
{
    public interface IOpenWeathermapService
    {
        /// <summary>
        /// Retrieves the data for the current weather
        /// </summary>
        /// <param name="cityId">German City</param>
        /// <returns>Some(data) for the current weather. None, when cityId is unknown</returns>
        public Task<Option<OpenWeathermapCurrent>> GetCurrentWeather(int cityId);

        /// <summary>
        /// Retrieves the data for the weatherforecast
        /// </summary>
        /// <param name="cityId">Id of the city</param>
        /// <returns>Some(data) for the weatherforecast. None, when the cityId is unknown</returns>
        public Task<Option<OpenWeathermapForecast>> GetWeatherforecast(int cityId);
    }
}