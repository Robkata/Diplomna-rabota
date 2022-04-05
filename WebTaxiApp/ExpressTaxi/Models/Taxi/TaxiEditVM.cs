using ExpressTaxi.Models.Brand;
using ExpressTaxi.Models.Driver;
using ExpressTaxi.Models.Option;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Models.Taxi
{
    public class TaxiEditVM
    {
        public TaxiEditVM()
        {
            Brands = new List<BrandChoiceVM>();
            Drivers = new List<DriverPairVM>();
        }
        [Key]

        public int Id { get; set; }
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        [Display(Name = "Engine")]
        public string Engine { get; set; }
        [Display(Name = "Extras")]
        public string Extras { get; set; }
        [Display(Name = "Year")]
        public DateTime Year { get; set; }
        [Display(Name = "Driver")]
        public int DriverId { get; set; }

        public virtual List<BrandChoiceVM> Brands { get; set; }
        public virtual List<DriverPairVM> Drivers { get; set; }

    }
}
