using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.CommercialVM
{
    public class ILCBillVModel
    {
        [Display(Name = "ILCB No")]
        public int ILCBNo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "LC ID")]
        public int ILCID { get; set; }
        [Display(Name = "Group")]
        public int GroupID { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public decimal BillTotalValue { get; set; }
        public string Type { get; set; }
      
        [Display(Name = "Select A/C")]
        public int DebitAccID { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        
        [Display(Name = "Sub Ledger")]
        public int SubLedgerID { get; set; }
        [Display(Name = "Sub Ledger Amount")]
        public decimal SubAmount { get; set; }
        [Display(Name = "Sub Ledger Total")]
        public decimal SubLedgerTotal { get; set; }
        [Display(Name = "Description")]
        public string SubDescription { get; set; }
        
        [Display(Name = "ILC No")]
        public string ILCNo { get; set; }
        [Display(Name = "AIt ILC No")]
        public string AitILCNo { get; set; }
        [Display(Name = "Supplier of Goods")]
        public string SupplierOfGoods { get; set; }
        [Display(Name = "LTR No")]
        public int LTRNo { get; set; }
    }
}