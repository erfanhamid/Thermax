using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class SalaryProcessViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Basic { get; set; }
        public decimal HRA { get; set; }
        public decimal MA { get; set; }
        public decimal CA { get; set; }
        public decimal Gross { get; set; }
        public decimal AdvanceAdjust { get; set; }
        public decimal Tax { get; set; }
        public decimal TMPhone { get; set; }
        public decimal PFOwn { get; set; }
        public decimal MessBill { get; set; }
        public decimal HouseRent { get; set; }
        public decimal UtilityBill { get; set; }
        public decimal CanteenBill { get; set; }
        public decimal SecurityReceive { get; set; }
        public decimal RevenueStamp { get; set; }
        public decimal OtherAdjust { get; set; }
        public decimal FSales { get; set; }
        public decimal Arear { get; set; }
        public decimal TotalAdition { get; set; }
        public decimal OverTimeTaka { get; set; }
        //public int Week { get; set; }
        public decimal EarnedSalary { get; set; }
        //public int Pay { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal PayableSalary { get; set; }
        public int DaysInMonth { get; set; }
        public int TotalPresent { get; set; }
        public double Loan { get; set; }
        public double TDS { get; set; }
        public decimal OtherAdj { get; set; }
        public decimal TA { get; set; }
    }
}