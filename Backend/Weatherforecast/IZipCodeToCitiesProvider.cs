using System.Collections.Generic;

namespace Backend.Weatherforecast
{
    public interface IZipcodeToCitiesProvider
    {
        /// <summary>
        /// All zipCodes (int) and the cities per zipCode (IEnumerable<string>)
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, IEnumerable<string>> Dictionary { get; }
    }
}