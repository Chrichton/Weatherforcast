using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Weatherforecast.Service
{
    public class WeatherService : IWeatherService
    {
        /// <summary>
        /// Retrieves the data for the weather
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>Current Weather-Data</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        /// <exception cref="ArgumentException">When city is unknown</exception>
        public async Task<Model> GetWeather(string city)
        {
            return await Task.FromResult(new Model());
        }

        /// <summary>
        /// Retrieves all cities for the supplied zipCode
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns>all cities for the supplied zipCode or emtpy when zipCode is unknown</returns>
        public async Task<IEnumerable<string>> GetCitiesForZipCode(string zipCode)
        {
            return await Task.FromResult(Enumerable.Empty<string>());
        }
    }
}
