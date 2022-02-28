using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales
{
    public class DueInvoice
    {
        public string InvoiceNo { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal DueAmount { get; set; }
        public DateTime Date { get; set; }
    }
}