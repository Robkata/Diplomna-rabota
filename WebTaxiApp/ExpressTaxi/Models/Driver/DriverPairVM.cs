using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Models.Driver
{
    public class DriverPairVM
    {
        public int Id { get; set; }
        [Display(Name = "Driver")]
        public string Name { get; set; }
    }
}
