using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.CommercialModule.CommercialVM
{
    public class ImportProformaInvoiceVM
    {
        [Display(Name = "IPI ID")]
        public int IPIId { get; set; }
        [Display(Name = "PI NO")]
        public string IPINO { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        [Display(Name = "IPI Date")]
        public DateTime IPIDate { get; set; }
        public int CompanyID { get; set; }
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        [Display(Name = "MOP")]
        public int MoPId { get; set; }
        [Display(Name = "Loading Port")]
        public int LoadingPortId { get; set; }
        [Display(Name = "Discharge Port")]
        public int DischargePortId { get; set; }
        [Display(Name = "Incoterms")]
        public int IncotermsId { get; set; }
        [Display(Name = "Beneficiary Bank")]
        public int BeneficiaryBankId { get; set; }
        [Display(Name = "Issuing Bank")]
        public int IssuingBankId { get; set; }
        [Display(Name = "Item Total")]
        public decimal ItemTotal { get; set; }
        [Display(Name = "Freight")]
        public decimal Freight { get; set; }
        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }
        [Display(Name = "Item")]
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        public int Carton { get; set; }
        public string LiterKg { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public string HSCode { get; set; }
        [Display(Name = "Packing")]
        public string PackingNote { get; set; }
        public int GroupId { get; set; }
        public string UOM { get; set; }
        [Display(Name = "Purchase Order No")]
        public int PurchaseOrderNo { get; set; }

        [Display(Name = "Supplier Account No.")]
        public string SupplierAccNo { get; set; }
        [Display(Name = "Partial Shipment")]
        public string PartialShipment { get; set; }
        [Display(Name = "Transhipment")]
        public string Transhipment { get; set; }
        [Display(Name = "Swift Code")]
        public string SwiftCode { get; set; }
        [Display(Name = "Rate Per Liter/KG")]
        public decimal RatePerLiter { get; set; }
        [Display(Name = "Bank Name")]
        public string SupBankName { get; set; }
        [Display(Name = "Bank Address")]
        public string SupBankAddress { get; set; }
    }
}