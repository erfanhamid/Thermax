using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ImportLCSummaryNewReport
    {
        public int ILCID { get; set; }
        public string ILCNO { get; set; }
        public DateTime ILCDate { get; set; }
        public string SupplierName { get; set; }
        public string ALCNO { get; set; }
        public DateTime ShipDate { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal FrieghtAmount { get; set; }
        public decimal Grandtotal { get; set; }
    }
}