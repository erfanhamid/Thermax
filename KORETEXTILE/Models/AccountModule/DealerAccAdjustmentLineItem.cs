using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("tblDealerAccAdjustmentLineItem")]
    public class DealerAccAdjustmentLineItem
    {
        [Required]
        [Key,Column("clmDAANo",Order =0)]
        public int DAANo { get; set; }
        [Required]
        [Key,Column("clmDealerID",Order =1)]
        public int DealerID { get; set; }
        [Column("clmAmount")]
        public decimal Amount { get; set; }
        [NotMapped]
        public string DealerName { get; set; }
    }
}