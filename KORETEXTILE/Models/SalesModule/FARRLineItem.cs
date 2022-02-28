using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table("tblRentRefundLineItem")]
    public class FARRLineItem
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clmFARRNo { get; set; }
        [Key]
        [Column(Order = 1)]
        public int clmRetailerID { get; set; }
        public string clmREfno { get; set; }
        public string clmDescriptions { get; set; }
        public decimal clmAmount { get; set; }
        public string clmDealerYN { get; set; }
        [NotMapped]
        public string RetailerName { get; set; }
    }
}