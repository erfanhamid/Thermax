using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Store_FG
{
    [Table("tblFGRPLine")]
    public class FGRPLineItem
    {
        [Key]
        [Column("clmFGRPNo", Order =0)]
        public int FGRPNo { set; get; }
        [Column("clmItemGrpID")]
        public int ItemGrpID { set; get; }
        [Key]
        [Column("clmItemID",Order = 1)]
        public int ItemID { set; get; }
        [Column("clmFGRPQty")]
        public decimal FGRPQty { set; get; }
        [Column("clmCostRt")]
        public decimal CostRt { set; get; }
        [Column("clmCostVal")]
        public decimal CostVal { set; get; }
    }
}