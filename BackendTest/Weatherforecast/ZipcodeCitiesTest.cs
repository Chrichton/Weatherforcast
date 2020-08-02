using Backend.Weatherforecast;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTest.Weatherforecast
{
    public class ZipcodeCitiesTest
    {
        [Fact]
        public void TestNoCity()
        {
            IEnumerable<string> city = ZipcodeCities.Dictionary.GetValueOrDefault("2103", null);

            Assert.Null(city);
        }

        [Fact]
        public void TestZipcodeCity()
        {
            IEnumerable<string> city = ZipcodeCities.Dictionary.GetValueOrDefault("21037", null);

            Assert.NotNull(city);
            Assert.Single(city);
            Assert.Equal("Hamburg", city.Single());
        }

        [Fact]
        public void TestZipcodeCities()
        {
            // zu "55767" gehören 20 City
            IEnumerable<string> city = ZipcodeCities.Dictionary.GetValueOrDefault("55767", null);

            Assert.NotNull(city);
            Assert.Equal(20, city.Count());
            Assert.Contains("Brücken", city);
        }
    }
}
