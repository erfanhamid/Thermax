using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class MonthWiseAttendanceSummary
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public int NoOfWorkingDays { get; set; }
        public int WeeklyHolidays { get; set; }
        public int GovernmentHoliDays { get; set; }
        public int LeaveTakes { get; set; }
        public int TotalPresent { get; set; }
        public int TotalAbsent { get; set; }
    }
}