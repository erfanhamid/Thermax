using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Transaction
{
    public class AdjustedAmountVModel
    {
        public int AANo { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
        public int InvoiceNo { get; set; }
        public int PVNo { get; set; }
        public decimal AdjustedAmount { get; set; }
        [Display(Name = "Amount")]
        public decimal ReceiveAmt { get; set; }
        public string Description { get; set; }
        [Display(Name = "Depot")]
        public int DepotId { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Zone")]
        public int ZoneId { get; set; }
        public string Area { get; set; }
        public decimal Due { get; set; }
        public string AccName { get; set; }
        public string ZoneName { get; set; }
        public string PayMode { set; get; }
        [Display(Name = "Due Amount")]
        public decimal DueAmount { get; set; }
        [Display(Name = "Advance Amount")]
        public decimal AdvanceAmount { get; set; }
    }
}