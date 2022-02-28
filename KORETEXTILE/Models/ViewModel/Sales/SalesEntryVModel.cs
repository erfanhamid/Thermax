using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales
{
    public class SalesEntryVModel
    {
        [Display(Name ="Invoice No")]
        public int InvoiceNo { set; get; }
        [Display(Name ="Date")]
        public DateTime SalesDate { set; get; }
        [Display(Name ="Depot")]
        public int CustBal { set; get; }
        [Display(Name ="SR ID")]
        public int SalesManID { set; get; }
        [Display(Name ="SR Name")]
        public string SalesmanName { get; set; }
        [Display(Name ="Dealer ID")]
        public int CustomerID { set; get; }
        [Display(Name ="Dealer Name")]
        public string CustomerName { get; set; }
        public string Zone { get; set; }
        public string Area { get; set; }
        [Display(Name ="Store")]
        public int StoreID { set; get; }
        public int Group { get; set; }
        [Display(Name ="Item")]
        public int ItemID { set; get; }
        [Display(Name ="PCs Per Ctn")]
        public int PCsPerCtn { get; set; }
        [Display(Name ="Available Ctn")]
        public double AvailableCtn { get; set; }
        [Display(Name ="Available PCs")]
        public double AvailablePCs { get; set; }
        [Display(Name ="Sold PCs Qty")]
        public double Qty { set; get; }
        [Display(Name ="Sold Ctn Qty")]
        public double QtyCtn { get; set; }
        [Display(Name ="Free Offer PCs Qty")]
        public double OfferQty { set; get; }
        [Display(Name ="Price Per PCs")]
        public decimal Price { set; get; }
        [Display(Name ="Total Value")]
        public decimal SalesValue { set; get; }
        [Display(Name ="Total PCs Qty")]
        public double TotalQty { get; set; }
        [Display(Name ="Invoice Total")]
        public decimal InvoiceTotal { get; set; }
        public decimal Balance { get; set; }
        [Display(Name ="Commission")]
        public decimal Commision { get; set; }
        [Display(Name ="Amount")]
        public decimal CommisionAmount { get; set; }
        public decimal Discount { get; set; }
        [Display(Name ="Amount")]
        public decimal DiscountAmount { get; set; }
        public decimal COGSValue { get; set; }
        public decimal COGOValue { get; set; }
        [Display(Name = "COGS Total")]
        public decimal COGSTotal { get; set; }
        [Display(Name = "COGO Total")]
        public decimal COGOTotal { get; set; }
        [Display(Name = "Net Of Commission")]
        public decimal NetOfCommision { get; set; }
        [Display(Name ="Net Invoice")]
        public decimal NetInvoice { get; set; }
    }
}