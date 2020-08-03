using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Weatherforecast.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [ApiController]
    [Route("")]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherController> logger;
        private readonly IWeatherService weatherService;

        public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService)
        {
            this.logger = logger;
            this.weatherService = weatherService;
        }

        [HttpGet("forecast/city/{city:minlength(1)}")]
        public IActionResult GetForecastByCity(string city)
        {
            return Ok(weatherService.GetWeather(city));
        }

        [HttpGet("forecast/zipcode/{zipcode:length(5)}")]
        public IActionResult GetForecastByZipCode(int zipCode)
        {
            Task<IEnumerable<string>> result = weatherService.GetCitiesForZipCode(zipCode);
            if (result.Result.Any())
                return Ok(result);

            return NotFound();
        }

        /*
        [HttpGet("forecast")]
        public IActionResult ZipCode([FromQuery] int zipCode)
        {
            Task<IEnumerable<string>> result = weatherService.GetCitiesForZipCode(zipCode);
            if (result.Result.Any())
                return Ok(result);

            return NotFound();
        }
        */

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
