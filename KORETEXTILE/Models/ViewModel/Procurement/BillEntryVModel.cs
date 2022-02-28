using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Procurement
{
    public class BillEntryVModel
    {
        [Display(Name = "GBE No")]
        public int GBENo { get; set; }
        [Display(Name = "Supplier Group")]
        public int SGId { get; set; }
        [Display(Name = "Depot")]
        public int DepotId { get; set; }
        [Display(Name = "Date")]
        public DateTime GBEDate { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        [Display(Name = "GBE Total")]
        public decimal GBETotal { get; set; }
        [Display(Name = "VAT %")]
        public decimal VAT { get; set; }
        [Display(Name = "VAT Amount")]
        public decimal VATAmount { get; set; }
        [Display(Name = "AIT %")]
        public decimal AIT { get; set; }
        [Display(Name = "AIT Amount")]
        public decimal AITAmount { get; set; }
        [Display(Name = "Total Payable")]
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        [Display(Name = "COA")]
        public int DebitAccId { get; set; }
        [Display(Name = "Cost Group")]
        public int CGId { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Description")]
        public string Descriptions { get; set; }
        [Display(Name = "Discount")]
        public decimal Discountamt { get; set; }
        [Display(Name = "Net Amount")]
        public decimal NetDiscountAmount { get; set; }
        [Display(Name = "VDS %")]
        public decimal VDS { get; set; }
        [Display(Name = "VDS Amount")]
        public decimal VDSAmount { get; set; }
        public decimal NetOfVatAmount { set; get; }
    }
}