using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales
{
    public class SalesReturnViewModel
    {
        [DisplayName("SRNo")]
        public int SRNo { get; set; }
        [DisplayName("Date")]
        public DateTime SRDate { get; set; }
        [DisplayName("Invoice No")]
        public int InvoiceNo { get; set; }
        [DisplayName("Customer")]
        public int CustomerId { get; set; }
        [DisplayName("Branch")]
        public int BranchId { get; set; }
        public int Store { get; set; }
        public string Description { get; set; }
        public decimal TotalCOGS { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalRtnAmount { get; set; }
        [DisplayName("Total Qty")]
        public decimal TotalQty { get; set; }
        [DisplayName("Total Vat")]
        public decimal TotalVatAmount { get; set; }
        [DisplayName("Total Discount")]
        public decimal TotalDiscountAmount { get; set; }
        [DisplayName("Item")]
        public int ItemID { get; set; }
        [DisplayName("Return Qty")]
        public decimal ReturnQty { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Cost { get; set; }
        public decimal Value { get; set; }
        public decimal Price { get; set; }
        public int SalesQty { get; set; }
        [DisplayName("Offer Qty")]
        public decimal OfferQty { get; set; }
        public decimal RtnOfferQty { get; set; }
        public int TSO { get; set; }
        public decimal VatPer { get; set; }
        public decimal DisPer { get; set; }
        public decimal NetAmount { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalValue { get; set; }
        [DisplayName("Unit Cost")]
        public decimal ClmCostPrice { get; set; }
    }
}