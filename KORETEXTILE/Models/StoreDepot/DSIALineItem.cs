using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.StoreDepot
{
    [Table("tblDSIALineItem")]
    public class DSIALineItem
    {
        [Required]
        [Key, Column("clmDSIANo",Order = 0)]
        public int DSIANo { get; set; }
        [Required]
       
        [Key, Column("clmItemID", Order = 1)]
        public int ItemID { get; set; }
        [Required]
        [Column("clmAdjQTY")]
        public int AdjQTY { get; set; }
        [Column("clmRate")]
        public decimal Rate { get; set; }
        [Column("clmValue")]
        public decimal Value { get; set; }
        //public decimal VatAmount { get; set; }
        [Column("clmCOGSRate")]
        public decimal COGSRate { get; set; }
        [Column("clmCOGSValue")]
        public decimal COGSValue { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        public int UnitPerCtn { get; set; }

        //[NotMapped]
        //public decimal AdjQTY1 { get; set; }
    }
}