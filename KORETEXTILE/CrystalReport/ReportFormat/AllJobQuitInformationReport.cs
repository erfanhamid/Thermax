using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class AllJobQuitInformationReport
    {
        public int JQINo { get; set; }
        public DateTime QuitDate { get; set; }
        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
        public string BrnachName { get; set; }
        public string FuncDesignation { get; set; }
        public string DesignationName { get; set; }
        public string RoL { get; set; }
        public string RefNo { get; set; }
        public string Descriptions { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}