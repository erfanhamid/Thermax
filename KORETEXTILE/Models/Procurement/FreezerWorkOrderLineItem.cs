using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement
{
    [Table("tblFWOLine")]
    public class FreezerWorkOrderLineItem
    {
        
        [Required]
        [Key, Column("clmWONo", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WONo { get; set; }
        [Required]
        [Key, Column("clmItemID", Order = 1)]
        public int ItemID { get; set; }
        [Required]
        [Column("clmItemRate")]
        public decimal UnitCost { get; set; }
        [Required]
        [Column("clmItemQty")]
        public decimal ItemQty { get; set; }
        [Required]
        [Column("clmItemValue")]
        public decimal TotalCost { get; set; }
        [Column("clmItemDescriptions")]
        public string Description { get; set; }
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
    }
}