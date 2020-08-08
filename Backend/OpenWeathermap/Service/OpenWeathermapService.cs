using LanguageExt;
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
        private readonly ICitynameToIdProvider cityToIdMapping;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpClient"></param>
        /// <param name="cityToIdMapping">The API needs a mapping from a city to its id</param>
        public OpenWeathermapService(ILogger<OpenWeathermapService> logger, HttpClient httpClient, ICitynameToIdProvider cityToIdMapping)
        {
            this.logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} must not be null");
            this.httpClient = httpClient ?? throw new ArgumentNullException($"{nameof(httpClient)} must not be null");
            this.cityToIdMapping = cityToIdMapping ?? throw new ArgumentNullException($"{nameof(cityToIdMapping)} must not be null");
        }

        /// <summary>
        /// Retrieves the data for the current weather
        /// </summary>
        /// <param name="city">German City</param>
        /// <returns>Some(data) for the current weather. None, when city is unknown</returns>
        public async Task<Option<OpenWeatherMapCurrent>> GetCurrentWeather(string city)
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
                logger.LogWarning($"unknown city: '{city}' requested", city);
                return Option<OpenWeatherMapCurrent>.None;
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
        /// <returns>Some(data) for the weatherforecast. None, when the city is unknown</returns>
        /// <exception cref="ArgumentNullException">When city is null</exception>
        public async Task<Option<OpenWeathermapForecast>> GetWeatherforecast(string city)
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
                logger.LogWarning($"unknown {nameof(city)} requested", city);
                return Option<OpenWeathermapForecast>.None;
            }

            string requestUri = $"{baseUrl}?appid={appId}&lang={language}&units={units}&id={cityId}";
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<OpenWeathermapForecast>(
                await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves the data for the current weather
        /// </summary>
        /// <param name="cityId">German City</param>
        /// <returns>Some(data) for the current weather. None, when cityId is unknown</returns>
        public async Task<Option<OpenWeatherMapCurrent>> GetCurrentWeather(int cityId)
        {
            const string baseUrl = "http://api.openweathermap.org/data/2.5/weather";

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
        /// <param name="cityId">Id of the city</param>
        /// <returns>Some(data) for the weatherforecast. None, when the cityId is unknown</returns>
        public async Task<Option<OpenWeathermapForecast>> GetWeatherforecast(int cityId)
        {
            const string baseUrl = "http://api.openweathermap.org/data/2.5/forecast";

            string requestUri = $"{baseUrl}?appid={appId}&lang={language}&units={units}&id={cityId}";
            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri).ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<OpenWeathermapForecast>(
                await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}