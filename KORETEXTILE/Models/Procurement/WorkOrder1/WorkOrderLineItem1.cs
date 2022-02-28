using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement.WorkOrder1      
{
    [Table("tblWorkOrderLineItem")]
    public class WorkOrderLineItem1 
    {
        [Key]
        [Column("clmWONo", Order = 0)]
        [Required]
        public int WONo { get; set; }
        [Key]
        [Column("clmItemID", Order = 1)]
        [Required]
        public int ItemID { get; set; }
        [Required]
        [Column("clmItemRate")]
        public decimal ItemRate { get; set; }
        [Required]
        [Column("clmItemQty")]
        public int ItemQty { get; set; }
        [Column("clmItemValue")]
        public decimal ItemValue { get; set; }
        [Required]
        [Column("clmItemDescriptions")]
        public string ItemDescriptions { get; set; }
        [Column("clmGroupID")]
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
    }
}