using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("tblImportLCLineItem")]
    public class ImportLCLineItem
    {
        [Key]
        [Column("clmILCID",Order = 0 )]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ILCID { get; set; }
        [Key]
        [Column("clmItemID", Order = 1)]
        public int ItemId { get; set; }
        //[Key]
        //[Column(Order = 2)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Sl { set; get; }
        [NotMapped]
        public string ItemName { get; set; }
        [Column("clmItemQty")]
        public decimal Qty { get; set; }
        [Column("clmItemRate")]
        public decimal Rate { get; set; }
        [Column("clmItemValue")]
        public decimal Value { get; set; }
        [Column("clmHSCODE")]
        public string HSCode { get; set; } 
        [Column("clmPacking")]
        public string PackingNote { get; set; }
    }
}