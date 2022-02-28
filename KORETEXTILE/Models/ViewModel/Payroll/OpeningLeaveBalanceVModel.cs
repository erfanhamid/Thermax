using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class OpeningLeaveBalanceVModel
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
        [DisplayName("Casual Leave")]
        public int CasualLeave { get; set; }
        [DisplayName("Sick Leave")]
        public int SickLeave { get; set; }
        [DisplayName("Earn Leave")]
        public int EarnLeave { get; set; }
        [DisplayName("Validate Until")]
        public DateTime ValidateUntil { get; set; }
    }
}