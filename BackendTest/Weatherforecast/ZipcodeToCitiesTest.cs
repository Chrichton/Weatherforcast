using Backend.Weatherforecast;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTest.Weatherforecast
{
    public class ZipcodeToCitiesTest
    {
        [Fact]
        public void TestNoCity()
        {
            IEnumerable<string> city = TestUtilities.ZipcodeToCities.Dictionary.GetValueOrDefault(2103, null);

            Assert.Null(city);
        }

        [Fact]
        public void TestZipcodeCity()
        {
            IEnumerable<string> city = TestUtilities.ZipcodeToCities.Dictionary.GetValueOrDefault(21037, null);

            Assert.NotNull(city);
            Assert.Single(city);
            Assert.Equal("Hamburg", city.Single());
        }

        [Fact]
        public void TestZipcodeCities()
        {
            Dictionary<int, IEnumerable<string>> dict = TestUtilities.ZipcodeToCities.Dictionary;
            IEnumerable<string> city = dict.GetValueOrDefault(TestUtilities.PlzWith20Cities, null);

            Assert.NotNull(city);
            Assert.Equal(20, city.Count());
            Assert.Contains("Brücken", city);
        }
    }
}
