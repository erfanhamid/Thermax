using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.CommercialModule.Import
{
    [Table("tblImportPI")]
    public class ImportProformaInvoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "IPI ID")]
        [Column("clmIPIID")]
        public int IPIId { get; set; }
        [Display(Name = "IPI NO")]
        [Column("clmIPINO")]
        public string IPINO { get; set; }
        [Display(Name = "Supplier")]
        [Column("clmSupplierID")]
        public int SupplierId { get; set; }
        [Display(Name = "IPI Date")]
        [Column("clmIPIDate")]
        public DateTime IPIDate { get; set; }
        [Required]
        public int CompanyID { get; set; }
        [Display(Name = "Currency")]
        [Column("clmCurrencyID")]
        public int CurrencyId { get; set; }
        [Display(Name = "MOP")]
        [Column("clmMoPID")]
        public int MoPId { get; set; }
        [Display(Name = "Loading Port")]
        [Column("clmLoadingPortID")]
        public int LoadingPortId { get; set; }
        [Display(Name = "Discharge Port")]
        [Column("clmDischargePortID")]
        public int DischargePortId { get; set; }
        [Display(Name = "Incoterms")]
        [Column("clmIncotermsID")]
        public int IncotermsId { get; set; }
        [Display(Name = "Beneficiary Bank")]
        [Column("clmBeneficiaryBankID")]
        public int BeneficiaryBankId { get; set; }
        [Display(Name = "Issuing Bank")]
        [Column("clmIssuingBankID")]
        public int IssuingBankId { get; set; }
        [Display(Name = "Item Total")]
        [Column("clmItemTotal")]
        public decimal ItemTotal { get; set; }
        [Display(Name = "Freight")]
        [Column("clmFreight")]
        public decimal Freight { get; set; }
        [Display(Name = "Grand Total")]
        [Column("clmGrandTotal")]
        public decimal GrandTotal { get; set; }
        //public int PurchaseOrderNo { get; set; }
        //public string PartialShipment { get; set; }
        //public string Transhipment { get; set; }
        //public string SwiftCode { get; set; }
        //public string SupplierAccNo { get; set; }
        //public string SupBankName { get; set; }
        //public string SupBankAddress { get; set; }  

    }
}