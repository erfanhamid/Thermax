using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class EmployeeSalaryAdditionForSpTableVModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Basic { get; set; }
        public decimal HRA { get; set; }
        public decimal MA { get; set; }
        public decimal CA { get; set; }
        public decimal Gross { get; set; }
        public decimal OtherAdj { get; set; }
        //public decimal TA { get; set; }
    }
}