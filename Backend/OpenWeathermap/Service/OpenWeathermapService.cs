﻿using LanguageExt;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.OpenWeathermap.Service
{
    public class OpenWeathermapService : IOpenWeathermapService
    {
        const string appId = "fcadd28326c90c3262054e0e6ca599cd";
        const string language = "de";
        const string units = "metric";

        private readonly ILogger<OpenWeathermapService> logger;
        private readonly HttpClient httpClient;
        private readonly ICitynameToId cityToIdMapping;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpClient"></param>
        /// <param name="cityToIdMapping">The API needs a mapping from a city to its id</param>
        public OpenWeathermapService(ILogger<OpenWeathermapService> logger, HttpClient httpClient, ICitynameToId cityToIdMapping)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.cityToIdMapping = cityToIdMapping ?? throw new ArgumentNullException(nameof(cityToIdMapping));
        }

        /// <summary>
        /// Retrieves the data for the current weather
        /// </summary>
        /// <param name="cityId">German City</param>
        /// <returns>Some(data) for the current weather. None, when cityId is unknown</returns>
        public async Task<Option<OpenWeathermapCurrent>> GetCurrentWeather(int cityId)
        {
            const string baseUrl = "http://api.openweathermap.org/data/2.5/weather";

            Uri requestUri = new Uri($"{baseUrl}?appid={appId}&lang={language}&units={units}&id={cityId}");

            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<OpenWeathermapCurrent>(
                await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves the data for the weatherforecast
        /// </summary>
        /// <param name="cityId">Id of the city</param>
        /// <returns>Some(data) for the weatherforecast. None, when the cityId is unknown</returns>
        public async Task<Option<OpenWeathermapForecast>> GetWeatherforecast(int cityId)
        {
            const string baseUrl = "http://api.openweathermap.org/data/2.5/forecast";

            Uri requestUri = new Uri($"{baseUrl}?appid={appId}&lang={language}&units={units}&id={cityId}");
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<OpenWeathermapForecast>(
                await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}