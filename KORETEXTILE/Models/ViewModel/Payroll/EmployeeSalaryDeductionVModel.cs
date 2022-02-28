using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class EmployeeSalaryDeductionVModel
    {
        public int EmpSalaryDeductionNo { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Grade {get;set;}
        public string Department {get;set;}
        public string Location {get;set;}
        public string Designation { get; set; }
        public decimal ProvidentFund { get; set; }
        public decimal LoanAdjustment { get; set; }
        public decimal MoneyRequisition { get; set; }
        public decimal VehicleAdjustment { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal Total { get; set; }
    }
}