using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class RMInentoryQtyBalancDetailReport
    {
        public string GroupName { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Uname { get; set; }
        public decimal RMOB { get; set; }
        public decimal GRN { get; set; }
        public decimal LCRN { get; set; }
        public decimal TOTALAVAILABLE { get; set; }
        public decimal RMC { get; set; }
        public decimal RMS { get; set; }
        public decimal RMSA { get; set; }
        public decimal TOTALISSUE { get; set; }
        public decimal CLOSINGBALANCE { get; set; }
    }
}