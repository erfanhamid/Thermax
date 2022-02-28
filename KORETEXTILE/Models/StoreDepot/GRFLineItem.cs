using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.StoreDepot  
{
    [Table("tblGRFLineItem")]
    public class GRFLineItem
    {
        [Key, Column("clmGRFNo", Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public int GRFNo { get; set; }
        [Column("clmItemGrpID")]
        public int GroupID { get; set; }
        [Key, Column("clmItemID", Order = 1)]
        [Required]
        
        public int ItemID { get; set; }
        [Required]
        [Column("clmQty")]
        public int Qty { get; set; }
        [Column("clmCostRt")]
        public decimal Rate { get; set; }
        [Column("clmCostVal")]
        public decimal Cost { get; set; }
        //public decimal UnitRetailPrice { get; set; }
        [NotMapped]
        public string ItemName {get;set;}
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string PackSize { get; set; }
    }
}