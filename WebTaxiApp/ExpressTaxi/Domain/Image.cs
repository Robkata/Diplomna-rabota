using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressTaxi.Domain
{
    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]

        public string Id { get; set; }
        [Required]
        [ForeignKey("Car img")]

        public int ProductId { get; set; }

        public virtual Taxi Taxi { get; set; }

        public string Extension { get; set; }
    }
}
