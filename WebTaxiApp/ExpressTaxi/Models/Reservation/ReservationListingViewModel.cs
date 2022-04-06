using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Models.Reservation
{
    public class ReservationListingViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Destination { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int TaxiId { get; set; }

        [Display(Name = "Taxi")]
        public string Taxi { get; set; }
        public string EventStart { get; set; }
        public string EventEnd { get; set; }
        public string EventPlace { get; set; }
        public string CustomerId { get; set; }

        [Display(Name = "Customer")]
        public string CustomerUsername { get; set; }
        public int TicketsCount { get; set; }
    }
}
