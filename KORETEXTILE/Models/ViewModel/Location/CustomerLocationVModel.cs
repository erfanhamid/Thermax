using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Location
{
    public class CustomerLocationVModel
    {
        [Key]
        [Required]
        [Display(Name ="Dealer Location No")]
        public int CLNo { get; set; }
        [Required]
        [Display(Name ="TSO Name")]
        public int TsoID { get; set; }
        [Required]
        [Display(Name ="Dealer Name")]
        public int CustomerID { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}