using ExpressTaxi.Models.Option;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Models.Reservation
{
    public class ReservationCreateBindingModel
    {
        public ReservationCreateBindingModel()
        {
            Options = new List<OptionPairVM>();
        }
        [Key]

        [Required]
        public int TaxiId { get; set; }
        public string Destination { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Passengers { get; set; }
        public int OptionId { get; set; }
        public virtual List<OptionPairVM> Options { get; set; }
        public string Status { get; set; }
    }
}
