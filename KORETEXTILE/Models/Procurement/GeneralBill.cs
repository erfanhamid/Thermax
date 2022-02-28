using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Procurement
{
    [Table("tblGeneralBills")]
    public class GeneralBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="GBE No")]
        [Column("clmGBENo")]
        public int GBENo { get; set; }
        [Display(Name = "Date")]
        [Column("clmGBEDate")]
        public DateTime GBEDate { get; set; }
        [Display(Name = "Depot")]
        [NotMapped]
        public int DepotId { get; set; }
        [Display(Name = "Supplier Group")]
        [NotMapped]
        public int SGId { get; set; }
        [NotMapped]
        public string SupplierGroup { get; set; }
        [Display(Name = "Supplier")]
        [Column("clmSupplierID")]
        public int SupplierId { get; set; }
        [NotMapped]
        public string SupplierName { get; set; }
        [Display(Name = "Reference No")]
        [Column("clmRefNo")]
        public string RefNo { get; set; }
        [Display(Name = "GBE Total")]
        [Column("clmGBETotal")]
        public decimal GBETotal { get; set; }
        //[Display(Name = "VAT %")]
        [NotMapped]
        public decimal VAT { get; set; }
        [Display(Name = "VAT Amount")]
        [Column("clmVATAmount")]
        public decimal VATAmount { get; set; }
        [Display(Name = "Net Discount")]
        [Column("NetofDiscount")]
        public decimal NetofDiscount { get; set; }
        //[Display(Name = "AIT %")]
        //[Column("clmAITAmount")]
        [NotMapped]
        public decimal AIT { get; set; }
        [NotMapped]
        public decimal VDS { get; set; }
        [Display(Name = "AIT Amount")]
        [Column("clmAITAmount")]
        public decimal AITAmount { get; set; }
        [Display(Name = "Total Payable")]
        [Column("clmNetAmount")]
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        [Required]
        public int clmCostCenterID { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
        
       // public decimal NetofDiscount { get; set; }
        
        public decimal Discountamt { get; set; }
        public decimal VDSAmount { get; set; }




    }
}