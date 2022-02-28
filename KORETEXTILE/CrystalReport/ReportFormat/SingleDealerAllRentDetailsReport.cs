using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleDealerAllRentDetailsReport
    {
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public string Depot { get; set; }
        public string AccountName { get; set; }
        public DateTime Date { get; set; }
        public int FRRNo { get; set; }
        public int RetailerID { get; set; }
        public string RetailerName { get; set; }
        public string Ref { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
    }
}