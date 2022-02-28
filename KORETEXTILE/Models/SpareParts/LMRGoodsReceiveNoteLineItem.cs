using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("LMRGoodsReceiveNoteLineItem")]
    public class LMRGoodsReceiveNoteLineItem
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int GRNNo { get; set; }
        public int ItemGrpID { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int ItemID { get; set; }
        public int UOMID { get; set; }
        [Required]
        public decimal Qty { get; set; }
        public decimal CostRt { get; set; }
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