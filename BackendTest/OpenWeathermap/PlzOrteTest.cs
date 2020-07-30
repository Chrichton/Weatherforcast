using Backend.OpenWeathermap;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTest.OpenWeathermap
{
    public class PlzOrteTest
    {
        [Fact]
        public void TestNoOrt()
        {
            IEnumerable<string> orte = PlzOrte.Dictionary.GetValueOrDefault("2103", null);

            Assert.Null(orte);
        }

        [Fact]
        public void TestPlzOrt()
        {
            // zu "55767" gehören 20 Orte

            IEnumerable<string> orte = PlzOrte.Dictionary.GetValueOrDefault("21037", null);

            Assert.NotNull(orte);
            Assert.Single(orte);
            Assert.Equal("Hamburg", orte.Single());
        }

        [Fact]
        public void TestPlzOrte()
        {
            // zu "55767" gehören 20 Orte
            IEnumerable<string> orte = PlzOrte.Dictionary.GetValueOrDefault("55767", null);

            Assert.NotNull(orte);
            Assert.Equal(20, orte.Count());
            Assert.Contains("Brücken", orte);
        }
    }
}
