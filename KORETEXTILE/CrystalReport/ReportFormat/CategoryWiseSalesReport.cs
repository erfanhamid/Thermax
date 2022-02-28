using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class CategoryWiseSalesReport
    {
        public int InvoiceNo { set; get; }
        public DateTime SalesDate { set; get; }
        public decimal InvoiceAmount { set; get; }
        public decimal CommissionAmt { set; get; }
        public decimal DiscountAmt { set; get; }
        public int SalesManID { set; get; }
        public string CustomerID { set; get; }
        public int CustBal { set; get; }
        public string Remarks { get; set; }
        public string Description { get; set; }
        public decimal ClmCOGSTotal { set; get; }
        public decimal DisAmount { set; get; }
        public decimal VatAmount { set; get; }
        public int OrderNumber { get; set; }
        public string BranchName { set; get; }
        public string SaleType { set; get; }
    }
}