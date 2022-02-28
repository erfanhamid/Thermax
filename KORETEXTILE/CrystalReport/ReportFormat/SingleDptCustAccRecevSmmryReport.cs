using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleDptCustAccRecevSmmryReport
    {
        public string ZoneName { set; get; }
        public string BranchName { set; get; }
        public int CustomerID { set; get; }
        public string CustomerName { set; get; }
        public string MobileNo { set; get; }
        public decimal RcvAmount { set; get; }
        
    }
}