using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class EmployeeSalaryAdditionVModel
    {
        [DisplayName("Employee ID")]
        public int EmployeeId { get; set; }
        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }
        public int Designation { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        public string Grade { get; set; }
        public int Department { get; set; }
        public string Location { get; set; }
        public decimal Basic { get; set; }
        [DisplayName("House Rent Allowence")]
        public decimal HRA { get; set; }
        [DisplayName("Medical Allowence")]
        public decimal MA { get; set; }
        [DisplayName("Conveyance Allowence")]
        public decimal CA { get; set; }
        public decimal Gross { get; set; }
        public decimal OtherAdj { get; set; }
        public decimal TA { get; set; }
        [DisplayName("Branch/Depot")]
        public int BranchID { get; set; }
    }
}