using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Location
{
    public class TsoCustomerTravelCheckpointVModel
    {
        [Key]
        [Required]
        [Display(Name ="TCT NO")]
        public int TCTNo { get; set; }
        [Required]
        [Display(Name ="Tso Name")]
        public int TsoID { get; set; }
        [Display(Name ="Dealer Name")]
        public int CustomerID { get; set; }
        [Display(Name ="Zone")]
        public int ZoneID { get; set; }
        [Display(Name = "Area")]
        public string AreaName { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
    }
}