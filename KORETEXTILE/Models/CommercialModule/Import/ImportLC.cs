using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("tblImportLC")]
    public class ImportLC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "ILC ID")]
        [Column("clmILCID")]
        public int ILCId { get; set; }
        [Required]
        [Display(Name = "Supplier")]
        [Column("clmSupplierID")]
        public int SupplierId { get; set; }
        [Required]
        [Display(Name = "LC Date")]
        [Column("clmILCDate")]
        public DateTime ILCDate { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "LC NO")]
        [Column("clmILCNO")]
        public string ILCNO { get; set; }
        [StringLength(50)]
        [Display(Name = "ALC NO")]
        [Column("clmIALCNO")]
        public string IALCNO { get; set; }
        [Required]
        [Display(Name = "Ship Date")]
        [Column("clmShipDate")]
        public DateTime ShipDate { get; set; }
        [Required]
        [Display(Name = "IPI ID")]
        [Column("clmIPIID")]
        public int IPIId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "PS")]
        [Column("clmPSHIPMENT")]
        public string PShipment { get; set; }
        [Required]
        [Display(Name = "Item Total")]
        [Column("clmItemTotal")]
        public decimal ItemTotal { get; set; }
        [Required]
        [Display(Name = "Freight")]
        [Column("clmFreight")]
        public decimal Freight { get; set; }
        [Required]
        [Display(Name = "Grand Total")]
        [Column("clmGrnadTotal")]
        public decimal GrandTotal { get; set; }
        [Column("clmStatus")]
        public string LCStatus { get; set; }
        [NotMapped]
        public int PI { get; set; }
        [NotMapped]
        public string PINo { get; set; }
        public decimal TotalMiscellaneousCost { get; set; }
        public bool IsClosed { get; set; }
    }
}