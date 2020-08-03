using System.Collections.Generic;

namespace Backend.Weatherforecast
{
    public interface IZipCodeToCitiesProvider
    {
        public IDictionary<int, IEnumerable<string>> GetDictionary();
    }
}