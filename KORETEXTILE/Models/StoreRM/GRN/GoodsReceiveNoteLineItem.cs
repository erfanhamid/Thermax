using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.StoreRM.GRN
{
    [Table("tblGoodsReceiveNoteLineItem")]
    public class GoodsReceiveNoteLineItem
    {
        [Key, Column("clmGRNNo", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int GRNNo { get; set; }
        [Column("clmItemGrpID")]
        public int ItemGrpID { get; set; }
        [Key, Column("clmItemID", Order = 1)]
        [Required]
        public int ItemID { get; set; }
        [Column("clmUOMID")]
        public int UOMID { get; set; }
        [Required]
        [Column("clmQty")]
        public decimal Qty { get; set; }
        [Column("clmCostRt")]
        public decimal CostRt { get; set; }
        [Column("clmCostVal")]
        public decimal CostVal { get; set; }   
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string Unit { get; set; }
        [NotMapped]
        public string PackSize { get; set; }
        [NotMapped]
        public string UoM { get; set; }
        [NotMapped]
        public double VatPer { get; set; }
        [NotMapped]
        public double AitPer { get; set; }
    }
}