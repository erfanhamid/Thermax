using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreDepot
{
    [Table("StockAdjustmentLineItem")]
    public class StockAdjustmentLineItem
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 0)]
        public int SANo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Value { get; set; }
        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        public string UOM { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
    }
}