using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Backend.OpenWeathermap
{
    public class OpenWeathermapService
    {
        private HttpClient httpClient;

        public OpenWeathermapService(HttpClient httpClient)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient) + " must not be null");

            this.httpClient = httpClient;
        }

        public async Task<string> GetWeatherforecast(string ort)
        {
            if (ort == null)
                throw new ArgumentNullException(nameof(ort) + " must not be null");

            var responseMessage = await httpClient.GetAsync("http://api.openweathermap.org/data/2.5/forecast?APPID=fcadd28326c90c3262054e0e6ca599cd&lang=de&units=metric&id=6547395");
            responseMessage.EnsureSuccessStatusCode();
            var result = await responseMessage.Content.ReadAsStringAsync();

            return result;
        }
    }
}

