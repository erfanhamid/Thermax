using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class DepotWiseDealerReceiveableReport
    {
        public string Depot { get; set; }
        public string ZoneName { get; set; }
        public int DID { get; set; }
        public string DealerName { get; set; }
        public decimal ReceiveableAmount { get; set; }
    }
}