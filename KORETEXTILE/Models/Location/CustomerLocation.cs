using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Location
{
    public class CustomerLocation
    {
        [Key]
        [Column(Order =0)]
        [Required]
        public int CLNo { get; set; }
        [Required]
        public int TsoID { get; set; }
        [Required]
        [Key]
        [Column(Order = 1)]
        public int CustomerID { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [NotMapped]
        public string TsoName { get; set; }
        [NotMapped]
        public string CustomerName { get; set; }
        [NotMapped]
        public int s1 { get; set; }

    }
}