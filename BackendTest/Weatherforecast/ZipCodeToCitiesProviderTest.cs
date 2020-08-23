using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTest.Weatherforecast
{
    public class ZipcodeToCitiesProviderTest
    {
        [Fact]
        public void TestLiveProvider()
        {
            IEnumerable<string> result = TestUtilities.ZipcodeToCitiesProvider.Dictionary[21037];
            Assert.Single(result);
            Assert.Equal("Hamburg", result.Single());
        }

        [Fact]
        public void TestId()
        {
            IEnumerable<string> result = 
                TestUtilities.ZipcodeToCitiesProvider.Dictionary[TestUtilities.PlzWith20Cities];

            Assert.Equal(20, result.Count());
            Assert.Equal("Abentheuer", result.First());
        }

        [Fact]
        public void TestUnknownId()
        {
            if (TestUtilities.ZipcodeToCitiesProvider.Dictionary.TryGetValue(2, out _))
            {
                Assert.False(true, "Test Failed");
            }      
        }
    }
}
