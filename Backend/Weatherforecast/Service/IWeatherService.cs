﻿using LanguageExt;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Weatherforecast.Service
{
    public interface IWeatherService
    {
        /// Retrieves the data for the weather
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>Some(data) for the current weather. None, when the city is unknown</returns>
        /// <exception cref="ArgumentException">When city is unknown</exception>
        public Task<Option<WeatherModel>> GetWeather(string city);

        /// <summary>
        /// Retrieves all cities for the supplied zipCode
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns>all cities for the supplied zipCode or emtpy when zipCode is unknown</returns>
        /// <summary>
        public Task<IEnumerable<string>> GetCitiesForZipCode(int zipCode);
    }
}