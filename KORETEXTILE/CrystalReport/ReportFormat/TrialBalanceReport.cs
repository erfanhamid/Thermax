using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TrialBalanceReport
    {
        public string Name { set; get; }
        public decimal Debit { set; get; }
        public decimal Credit { set; get; }
        //public string AccType { set; get; }
        public string RootAccountType { set; get; }
        public string ParentName { set; get; }
    }
}