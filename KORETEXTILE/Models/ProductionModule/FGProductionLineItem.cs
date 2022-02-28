using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ProductionModule
{
    [Table("tblProductionLineItem")]
    public class FGProductionLineItem
    {
        [Key]
        [Column("clmFGPNo",Order =0)]
        public int FGPNo { set; get; }
        [Key]
        [Column("clmItemID",Order = 1)]
        public int ItemID { set; get; }
        [Column("clmQty")]
        public decimal Qty { set; get; }
        [NotMapped]
        public string ItemName { set; get; }
        [NotMapped]
        public int GroupId { set; get; }
        [Column("clmCogsRate")]
        public decimal CogsRate { set; get; }
        [Column("clmCogsTotal")]
        public decimal CogsTotal { set; get; }
        [NotMapped]
        public string PacSize { get; internal set; }
        [NotMapped]
        public decimal PCPerCtn { get; set; }
    }
}