using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TSOWiseSaleSummaryReport
    {
        public string BranchName { set; get; }
        public string ZoneName { set; get; }
        public int CustomerID { set; get; }
        public string CustomerName { set; get; }
        public string TsoName { set; get; }
        public int TsoId { set; get; }
        public decimal InvoiceAmount { set; get; }
        public decimal VatAmount { set; get; }
        public decimal DiscountAmt { set; get; }
        public decimal NetInvoice { set; get; }
    }
}