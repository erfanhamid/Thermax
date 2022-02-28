using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Procurement
{
    public class FPBVModel
    {
        [DisplayName("Bill No")]
        public int BillNo { set; get; }
        [DisplayName("Date")]
        public DateTime PDate { set; get; }
        [DisplayName("Supplier")]
        public int SupplierID { set; get; }
        public decimal BillTotalValue { set; get; }
        [DisplayName("Reference No")]
        public string PurRef { set; get; }
        [DisplayName("Description")]
        public string PurDescription { set; get; }
        public decimal DiscountAmt { set; get; }
        public decimal TotalDiscount { set; get; }
        [DisplayName("Vat Amount")]
        public decimal VatAmount { set; get; }
        [DisplayName("VDS Amount")]
        public decimal VDAAmount { set; get; }
        [DisplayName("AIT Amount")]
        public decimal AitAmount { set; get; }
        [DisplayName("Total Payable")]
        public decimal TotalPayable { set; get; }
        public decimal PaidAmount { set; get; }
        [DisplayName("FRN")]
        public int FRNNo { set; get; }
        [DisplayName("Supplier Group")]
        public int SGroupId { set; get; }
        [DisplayName("Work Order")]
        public int WorkOrderId { set; get; }
        [DisplayName("Work Order No")]
        public int WorkOrderNo { set; get; }
        public decimal Total { set; get; }
        public decimal Vat { set; get; }
        [DisplayName("AIT")]
        public decimal Ait { set; get; }
        [DisplayName("FPB Total")]
        public decimal FPBTotal { set; get; }
        public decimal Discount { set; get; }
        [DisplayName("Net Amount")]
        public decimal NetAmount { set; get; }
        [DisplayName("Vat (%)")]
        public decimal VatPercent { set; get; }
        [DisplayName("Net Of Vat Amount")]
        public decimal NetOfVatAmount { set; get; }
        [DisplayName("AIT (%)")]
        public decimal AitPercent { set; get; }
        [DisplayName("Net Of AIT Amount")]
        public decimal NetOfAitAmt { set; get; }
        [DisplayName("Bill Date")]
        public DateTime BillDate { set; get; }
        [DisplayName("Select Debit Account")]
        public int DebitAccount { get; set; }
    }
}