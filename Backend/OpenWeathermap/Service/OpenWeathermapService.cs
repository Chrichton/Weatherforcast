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
        private readonly ICityToIdProvider cityToIdMapping;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpClient"></param>
        /// <param name="cityToIdMapping">The API needs a mapping from a city to its id</param>
        public OpenWeathermapService(ILogger<OpenWeathermapService> logger, HttpClient httpClient, ICityToIdProvider cityToIdMapping)
        {
            this.logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} must not be null");
            this.httpClient = httpClient ?? throw new ArgumentNullException($"{nameof(httpClient)} must not be null");
            this.cityToIdMapping = cityToIdMapping ?? throw new ArgumentNullException($"{nameof(cityToIdMapping)} must not be null");
        }

        /// <summary>
        /// Retrieves the data for the current weather
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>Current Weather-Data</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        /// <exception cref="ArgumentException">When city is unknown</exception>
        public async Task<OpenWeatherMapCurrent> GetCurrentWeather(string city)
        {
            if (city == null)
            {
                logger.LogError("{nameof(city)} was null");
                throw new ArgumentNullException($"{nameof(city)} must not be null");
            }

            const string baseUrl = "http://api.openweathermap.org/data/2.5/weather";

            int cityId;
            if (!cityToIdMapping.GetDictionary().TryGetValue(city, out cityId))
            {
                logger.LogError($"unknown {nameof(city)} requested", city);
                throw new ArgumentException("Der Ort ist unbekannt", nameof(city));
            }

            string requestUri = $"{baseUrl}?appid={appId}&lang={language}&units={units}&id={cityId}";
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<OpenWeatherMapCurrent>(
                await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves the data for the weatherforecast
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>data for the weaterforecast</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        /// <exception cref="ArgumentException">When city is unknown</exception>
        public async Task<OpenWeathermapForecast> GetWeatherforecast(string city)
        {
            if (city == null)
            {
                logger.LogError("{nameof(city)} was null");
                throw new ArgumentNullException($"{nameof(city)} must not be null");
            }

            const string baseUrl = "http://api.openweathermap.org/data/2.5/forecast";

            int cityId;
            if (!cityToIdMapping.GetDictionary().TryGetValue(city, out cityId))
            {
                logger.LogError($"unknown {nameof(city)} requested", city);
                throw new ArgumentException("Der Ort ist unbekannt", nameof(city));
            }

            string requestUri = $"{baseUrl}?appid={appId}&lang={language}&units={units}&id={cityId}";
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<OpenWeathermapForecast>(
                await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}