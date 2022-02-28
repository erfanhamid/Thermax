using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales
{
    public class OfferVModel
    {
        public int ID { get; set; }
        [Display(Name ="Offer No")]
        public int OfferNo { get; set; }
        [Display(Name = "Offer Code")]
        public string OfferCode { get; set; }
        [Display(Name = "Offer Date")]
        public DateTime OfferDate { get; set; }
        [Display(Name = "Offer Valid From Date")]
        public DateTime ValidFrom { get; set; }
        [Display(Name = "Offer Valid To Date")]
        public DateTime ValidTo { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }
        [Display(Name = "Sales Amount")]
        public decimal SalesAmount { get; set; }
        [Display(Name = "Sales Percent")]
        public decimal SalesPercent { get; set; }
        [Display(Name = "Sales Category")]
        public string SalesCategory { get; set; }
        [Display(Name = "Item Name")]
        public int ItemID { get; set; }
        [Display(Name = "Unit")]
        public decimal Unit { get; set; }
        [Display(Name = "Free Unit")]
        public decimal FreeUnit { get; set; }
        [Display(Name = "Customer Name")]
        public int CustomerID { get; set; }
        [Display(Name = "Discount On Total Sale")]
        public decimal DiscountOnTotalSale { get; set; }
    }
}