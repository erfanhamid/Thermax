using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.StoreRM.IRM
{
    [Table("tblIRMLineItem")]
    public class IRMLineItem
    {
        
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column("clmIRMNo", Order = 0)]
        public int IRMID { get; set; }  
        [Required]
        [Column("clmGrpID")]
        public int GroupID { get; set; }    
        [Required]
        [Key, Column("clmItemID", Order = 1)]
        public int ItemID { get; set; }
        [Required]
        [Column("clmUomID")]
        public int UomID { get; set; }
        [Required]
        [Column("clmStockQt")]
        public decimal StockQty { get; set; }   
        [Required]
        [Column("clmQty")]
        public decimal Qty { get; set; }
        [Required]
        [Column("clrRate")]
        public decimal Rate { get; set; }
        [Required]
        [Column("clmValue")]
        public decimal Value { get; set; }  
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string UoMName { get; set; }
        [NotMapped]
        public string PackSize { get; set; }
    }
}