using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class LeaveStatus
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public int CasualAvailable { get; set; }
        public int CasualUsed { get; set; }
        public int CasualBalance { get; set; }
        public int EarnedAvailable { get; set; }
        public int EarnedUsed { get; set; }
        public int EarnedBalance { get; set; }

    }
}