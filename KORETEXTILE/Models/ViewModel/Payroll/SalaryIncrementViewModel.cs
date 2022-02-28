using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class SalaryIncrementViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public decimal Gross { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string WorkStation { get; set; }
        public string DepartmentName { get; set; }
        public string FuncDesignation { get; set; }
        public string Section { get; set; }

       
        public int BranchId { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public int FuncDesignationId { get; set; }
        public int SectionId { get; set; }
        public int CompanyId { get; set; }
    }
}