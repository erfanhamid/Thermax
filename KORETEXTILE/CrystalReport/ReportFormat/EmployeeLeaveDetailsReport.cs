using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmployeeLeaveDetailsReport
    {
        public int ELRNo { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string WorkStation { get; set; }
        public string LeaveType { get; set; }
        public Int16 LeaveDays { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Descriptions { get; set; }
    }
}