using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SPVoucherPreviewReport
    {
        public int PBID { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string SGroup { get; set; }
        public string Supplier { get; set; }
        public string Account { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public int BilllNo { get; set; }
        public string BillType { get; set; }
        public decimal Amount { get; set; }
    }
}