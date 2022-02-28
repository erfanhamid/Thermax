using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SupplierwisewosummaryReport
    {
        public int WONO { get; set; }
        public DateTime WODATE { get; set; }
        public int SUPPLIERID { get; set; }
        public string SUPPLIERNAME { get; set; }
        public int ITEMID { get; set; }
        public string ITEMNAME { get; set; }
        public string UNMAE { get; set; }
        public decimal ITEMQTY { get; set; }
        public decimal ITEMRATE { get; set; }
        public decimal ITEMVALUE { get; set; }
    }
}