using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleDepotRetailerReport
    {
        public int Groupid { get; set; }
        public string BrnachName { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int Id { get; set; }
        public string RetailerName { get; set; }
        public string MobileNo { get; set; }
        public string Billto { get; set; }
    }
}