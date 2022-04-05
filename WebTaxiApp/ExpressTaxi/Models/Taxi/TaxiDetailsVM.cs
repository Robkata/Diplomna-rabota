using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Models.Taxi
{
    public class TaxiDetailsVM
    {
        public int Id { get; set; }

        [Display(Name = "Модел")]
        public string Brand { get; set; }
        [Display(Name = "Двигател")]
        public string Engine { get; set; }
        [Display(Name = "Екстри")]
        public string Extras { get; set; }
        [Display(Name = "Година на производство")]
        public DateTime Year { get; set; }
        [Display(Name = "Шофьор на таксито")]
        public string Driver { get; set; }
    }
}
