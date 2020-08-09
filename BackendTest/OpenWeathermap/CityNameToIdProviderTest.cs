using Backend.OpenWeathermap;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class CityNameToIdProviderTest
    {
        [Fact]
        public void TestLiveProvider()
        {
            Assert.Equal(2911298, new CitynameToIdProvider().GetDictionary()["Hamburg"]);
        }

        [Fact]
        public void TestProvider()
        {
            var dict = new Dictionary<string, int> { { "Hamburg", 1} };
            var provider = new CitynameToIdProvider(dict);
            Assert.Equal(1, provider.GetDictionary()["Hamburg"]);

            int id;
            if (provider.GetDictionary().TryGetValue("Hambur", out id))
            {
                Assert.False(true, "Test Failed");
            }
        }
    }
}
