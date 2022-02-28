using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("DSIAdjustmentLineItem")]
    public class DSIAdjustmentLineItem
    {
        [Key, Column(Order =0)]
        [Required]
        public int DSIANo { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int DealerID { get; set; }
        public decimal Amount { get; set; }
        [NotMapped]
        public string DealerName { get; set; }
    }
}