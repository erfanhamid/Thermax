using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SinglestorermamountbalanceReport
    {
        public string GroupName { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string UName { get; set; }
        public decimal RMBALANCEQTY { get; set; }
        public decimal RMBALANCEAMOUNT { get; set; }
    }
}