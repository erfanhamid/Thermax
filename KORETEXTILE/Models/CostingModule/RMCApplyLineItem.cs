using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CostingModule
{
    [Table("tblRMCApplyLineItem")]
    public class RMCApplyLineItem
    {
        [Key]
        [Column("clmRMCNo", Order = 0)]

        public int RMCNo { set; get; }
        [Key]
        [Column("clmItemID", Order = 1)]

        public int ItemID { set; get; }
        [Column("clmItemQty")]
        public decimal ItemQty { set; get; }
        public decimal RmcRate { get; set; }
        public decimal RmcValue { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public decimal UnitStanCost { set; get; }
        [NotMapped]
        public decimal TotalStanCost { set; get; }
        [NotMapped]
        public string PacSize { get; set; }
    }
}