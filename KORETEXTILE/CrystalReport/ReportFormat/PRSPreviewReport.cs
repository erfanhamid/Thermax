using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class PRSPreviewReport
    {
        public int PRID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public DateTime Tdate { get; set; }
        public int ReceiveAccountID { get; set; }
        public string Name { get; set; }
        public decimal ReturnAmount { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
    }
}