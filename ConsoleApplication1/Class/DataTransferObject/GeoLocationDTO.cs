using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GeoLocationDTO
    {
        /// <summary>
        /// Konum enlem bilgisi.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Konum boylam bilgisi.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Konum adres bilgisi.
        /// </summary>
        public string Address { get; set; }
    }
}
