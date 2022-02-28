using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class UserWiseLogReport
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string FunctionalDesignation { get; set; }
        public int DocID { get; set; }
        public string DocType { get; set; }
        public string Action { get; set; }
        public DateTime TransectionDate { get; set; }
        public DateTime UserActualDate { get; set; }
    }
}