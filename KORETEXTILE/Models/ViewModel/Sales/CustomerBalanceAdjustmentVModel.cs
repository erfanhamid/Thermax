using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Sales
{
    public class CustomerBalanceAdjustmentVModel
    {
        public int CAANo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Depot")]
        public int BranchId { get; set; }
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }
        [Display(Name = "Total Amount")]
        public decimal TotalAAmount { get; set; }
        [Display(Name = "Invoice No")]
        public int InvoiceNo { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Due Amount")]
        public decimal DueAmount { get; set; }
        [Display(Name = "After Adjustment")]
        public decimal AfterAdjustment { get; set; }
    }
}