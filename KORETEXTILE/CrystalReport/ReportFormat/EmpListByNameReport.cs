using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmpListByNameReport
    {
        public string BranchName { set; get; }
        public int EmployeeId { set; get; }
        public string EmpGrade { set; get; }
        public string Designation { set; get; }
        public string FuncDesignation { set; get; }
        public string DeptInfo { set; get; }
        public string Mobile { set; get; }
        public string EmpName { set; get; }
    }
}