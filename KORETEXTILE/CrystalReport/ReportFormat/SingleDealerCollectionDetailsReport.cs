using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleDealerCollectionDetailsReport
    {
        public int RPID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
        public string AccountGroup { get; set; }        
    }
}