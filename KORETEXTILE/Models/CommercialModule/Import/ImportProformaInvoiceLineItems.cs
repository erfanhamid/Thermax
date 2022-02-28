using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.CommercialModule.Import
{
    [Table("tblImportPILineItem")]
    public class ImportProformaInvoiceLineItems
    {
        [Key,Column("clmIPIID", Order =0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IPIId { get; set; }
        [Key,Column("clmItemID", Order = 1)]
        [Display(Name = "Item")]
        public int ItemId { get; set; }
        [Column("clmQty")]
        public decimal Qty { get; set; }
        [Column("clmRate")]
        public decimal Rate { get; set; }
        [Column("clmValue")]
        public decimal Value { get; set; }
        [Column("clmHSCode")]
        public string HSCode { get; set; }
        [Display(Name = "Packing")]
        [Column("clmPackingNote")]
        public string PackingNote { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        public string GroupName { get; set; }

    }
}