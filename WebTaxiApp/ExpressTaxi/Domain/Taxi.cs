using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Domain
{
    public class Taxi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Required]
        public int TaxiId { get; set; }
        [Required]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        [Required]
        public string ImageId { get; set; }
        public virtual Image Image { get; set; }
        public string Engine { get; set; }
        public string Extras { get; set; }
        public DateTime Year { get; set; }
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}