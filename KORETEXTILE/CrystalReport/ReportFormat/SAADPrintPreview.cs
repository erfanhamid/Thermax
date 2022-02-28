using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SAADPrintPreview
    {
        public int SAAdjustmentNo { get; set; }
        public DateTime SAADate { get; set; }
        public int LedgerACCID { get; set; }
        public string Name { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public decimal SAAdjAmount { get; set; }
        public string Note { get; set; }
        public string RefNo { get; set; }
        public string BillType { get; set; }
        public int BillNo { get; set; }
    }
}