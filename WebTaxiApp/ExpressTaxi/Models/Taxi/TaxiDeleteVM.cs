using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Models.Taxi
{
    public class TaxiDeleteVM
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Engine { get; set; }
        public string Extras { get; set; }
        public DateTime Year { get; set; }
        public string Driver { get; set; }
    }
}