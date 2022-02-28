using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleBranchEPSReport
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string FuncDesig { get; set; }
        public string BranchName { get; set; }
        public string GradeName { get; set; }
        public string DepartmentName { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HRAllowance { get; set; }
        public decimal MedAllowance { get; set; }
        public decimal ConveanceAllowance { get; set; }
        public decimal SpecialAllowance { get; set; }
    }
}