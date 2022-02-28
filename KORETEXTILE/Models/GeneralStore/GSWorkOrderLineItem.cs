using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.GeneralStore
{
    [Table("GSWorkOrderLineItem")]
    public class GSWorkOrderLineItem
    {
        [Required]
        [Key, Column("WONo", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WONo { get; set; }
        [Required]
        [Key, Column("ItemID", Order = 1)]
        public int ItemID { get; set; }
        [Required]
        [Column("ItemRate")]
        public decimal UnitCost { get; set; }
        [Required]
        [Column("ItemQty")]
        public decimal ItemQty { get; set; }
        [Required]
        [Column("ItemValue")]
        public decimal TotalCost { get; set; }
        [Column("ItemDescriptions")]
        public string Specification { get; set; }
        [Column("GroupID")]
        public int GroupID { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string Unit { get; set; }
        [NotMapped]
        public decimal RevQty { get; set; }
        [NotMapped]
        public decimal AvaQty { get; set; }
        [NotMapped]
        public int UnitId { get; set; }
        [NotMapped]
        public string PackSize { get; set; }
        [NotMapped]
        public double VatPerc { set; get; }
        [NotMapped]
        public double AitPerc { set; get; }
        [NotMapped]
        public double VDAPerc { set; get; }
    }
}