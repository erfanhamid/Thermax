using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmployeeAttandanceLog
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public TimeSpan Late { get; set; }
        public TimeSpan OverTime { get; set; }
        public TimeSpan EarlyLeave { get; set; }
    }
}