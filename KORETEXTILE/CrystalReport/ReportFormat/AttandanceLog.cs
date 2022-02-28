using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class AttandanceLog
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime CheckIn { get; set; }
    }
}