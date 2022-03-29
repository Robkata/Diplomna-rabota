using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Domain
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Taxi> Taxies { get; set; }
    }
}
