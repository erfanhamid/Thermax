using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TsoWiseCustDetailsReport
    {
        public int CustomerId { set; get; }
        public string Name { set; get; }
        public string PhoneNo { set; get; }
        public string Email { set; get; }
        public double CreditLimit { set; get; }
        public int CreditDays { set; get; }
        public string TsoName { set; get; }
    }
}