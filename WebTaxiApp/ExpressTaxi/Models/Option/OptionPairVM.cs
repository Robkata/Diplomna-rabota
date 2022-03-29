using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Models.Option
{
    public class OptionPairVM
    {
        public int Id { get; set; }
        [Display(Name = "Option")]
        public string Name { get; set; }
    }
}
