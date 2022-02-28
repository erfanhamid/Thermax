using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class BankWiseLATRABSReport
    {
        public int LTRID { get; set; }
        public string LTRNo { get; set; }
        public decimal TotalAmount { get; set; }
        public string Bank { get; set; }
    }
}