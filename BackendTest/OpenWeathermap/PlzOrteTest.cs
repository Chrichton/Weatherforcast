using Backend.OpenWeathermap;
using System.Collections.Generic;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class PlzOrteTest
    {
        [Fact]
        public void TestPlzOrte()
        {
            string ort = PlzOrte.Dictionary.GetValueOrDefault("21037", "");
            Assert.Equal("Hamburg", ort);
        }
    }
}
