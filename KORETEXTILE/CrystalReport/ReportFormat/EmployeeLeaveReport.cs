using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmployeeLeaveReport
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Int16 LeaveId { get; set; }
        public string Designation { get; set; }
        public string WorkStation { get; set; }
        public string LeaveType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime endDate { get; set; }
        public Int16 LeaveDays { get; set; }
        public string Descriptions { get; set; }

    }
}