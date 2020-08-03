using Backend.Weatherforecast.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BackendTest.Weatherforecast.Service
{
    public class WeatherModelExtensionsTest
    {
        [Fact]
        public void TestCalculateAverageTemperatureNoData()
        {
            var model = new WeatherModel(new Weather(), new Weather[] { });
            float result = model.CalculateAverageTemperature();

            Assert.Equal(0, result);
        }

        [Fact]
        public void TestCalculateAverageTemperature()
        {
            var weather1 = new Weather { Temperature = 27.5f };
            var weather2 = new Weather { Temperature = 23.5f };
            
            var model = new WeatherModel(new Weather(), new Weather[] { weather1, weather2 });
            float result = model.CalculateAverageTemperature();

            Assert.Equal(51/2f, result);
        }

        [Fact]
        public void TestCalculateAverageHumidityNoData()
        {
            var model = new WeatherModel(new Weather(), new Weather[] { });
            float result = model.CalculateAverageHumidity();

            Assert.Equal(0, result);
        }

        [Fact]
        public void TestCalculateAverageHumidity()
        {
            var weather1 = new Weather { Humidity = 5 };
            var weather2 = new Weather { Humidity = 6 };

            var model = new WeatherModel(new Weather(), new Weather[] { weather1, weather2 });
            float result = model.CalculateAverageHumidity();

            Assert.Equal(11/2f, result);
        }
    }
}
