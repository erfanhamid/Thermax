using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Location
{
    [Table("TsoCustomerTravelCheckpoinTs")]
    public class TsoCustomerTravelCheckpoint
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Key, Column(Order = 0)]
        [Required]
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TCTNo { get; set; }
        [Required]
        public int TsoID { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int CustomerID { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int ZoneID { get; set; }
        public string Area { get; set; }
        public DateTime PreviousDate { get; set; }
        public bool IsVisit { set; get; }
    }
}