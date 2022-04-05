using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Models.Taxi
{
    public class TaxiAllVM
    {
        public int Id { get; set; }
        [Display(Name = "Taxi")]
        public int TaxiId { get; set; }
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public string Brand { get; set; }
        [Display(Name = "Image Picture")]
        public string ImageUrl { get; set; }
        [Display(Name = "Engine")]
        public string Engine { get; set; }

        [Display(Name = "Extras")]
        public string Extras { get; set; }
        [Display(Name = "Year")]
        public string Year { get; set; }
        [Display(Name = "Driver")]
        public int DriverId { get; set; }
        public string Driver { get; set; }
    }
}
