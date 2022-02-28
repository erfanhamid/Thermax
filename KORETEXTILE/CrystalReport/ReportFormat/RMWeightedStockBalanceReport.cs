using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class RMWeightedStockBalanceReport
    {
        public string GroupName { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string UName { get; set; }
        public decimal RMOBQY { get; set; }
        public decimal RMOBAMOUNT { get; set; }
        public decimal OBRATE { get; set; }
        public decimal RECEIVEQTY { get; set; }
        public decimal RECEIVEAMOUNT { get; set; }
        public decimal RECEIVERATE { get; set; }
        public decimal ISSUEQTY { get; set; }
        public decimal ISSUEAMOUNT { get; set; }
        public decimal ISSUERATE { get; set; }
        public decimal BALANCEQTY { get; set; }
        public decimal BALANCEAMOUNT { get; set; }
        public decimal BALANCERATE { get; set; }
    }
}