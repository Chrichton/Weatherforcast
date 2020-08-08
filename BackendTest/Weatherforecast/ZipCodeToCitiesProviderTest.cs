using Backend.Weatherforecast;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTest.Weatherforecast
{
    public class ZipCodeToCitiesProviderTest
    {
        [Fact]
        public void TestLiveProvider()
        {
            IEnumerable<string> result = new ZipCodeToCitiesProvider().GetDictionary()[21037];
            Assert.Single(new ZipCodeToCitiesProvider().GetDictionary()[21037]);
            Assert.Equal("Hamburg", result.Single());
        }

        [Fact]
        public void TestId()
        {
            var dictionary = new Dictionary<int, IEnumerable<string>>
            {
                { 1, new [] { "Ort1", "Ort2"} }
            };

            var provider = new ZipCodeToCitiesProvider(dictionary);
            IEnumerable<string> result = provider.GetDictionary()[1];

            Assert.Equal(2, result.Count());
            Assert.Equal("Ort1", result.First());
        }

        [Fact]
        public void TestUnknownId()
        {
            var dictionary = new Dictionary<int, IEnumerable<string>>
            {
                { 1, new [] { "Ort1", "Ort2"} }
            };

            var provider = new ZipCodeToCitiesProvider(dictionary);

            IEnumerable<string> value;
            if (provider.GetDictionary().TryGetValue(2, out value))
            {
                Assert.False(true, "Test Failed");
            }      
        }
    }
}
