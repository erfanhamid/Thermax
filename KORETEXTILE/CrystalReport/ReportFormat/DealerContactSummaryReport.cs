using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DealerContactSummaryReport
    {
        public int Id { get; set; }
        public string  CustomerName { get; set; }
        public string ConPerson { get; set; }
        public string MobileNo { get; set; }
        public string Billto { get; set; }
        public string Depot { get; set; }
    }
}