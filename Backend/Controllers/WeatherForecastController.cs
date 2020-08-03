using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.OpenWeathermap.Service;
using Backend.Weatherforecast.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> logger;
        private readonly IMapper mapper;
        private readonly IWeatherService weatherService;
        private readonly IOpenWeathermapService openWeathermapService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper,
            IWeatherService weatherService, IOpenWeathermapService openWeathermapService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.weatherService = weatherService;
            this.openWeathermapService = openWeathermapService;
        }

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
