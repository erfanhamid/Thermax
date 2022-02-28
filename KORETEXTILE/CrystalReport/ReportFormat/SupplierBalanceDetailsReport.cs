using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SupplierBalanceDetailsReport
    {
        public int groupid { get; set; }
        public int supplierid { get; set; }
        public string suppname { get; set; }
        public decimal OB { get; set; }
        public decimal GPB { get; set; }
        public decimal GSB { get; set; }
        public decimal FPB { get; set; }
        public decimal LCSPAmount { get; set; }
        public decimal MRPA { get; set; }
        public decimal CSAA { get; set; }
        public decimal SPV { get; set; }
        public decimal PFRS { get; set; }
        public decimal SAAD { get; set; }

    }
}