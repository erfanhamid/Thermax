using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class OpeningLeaveBalanceTableVModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int CasualLeave { get; set; }
        public int SickLeave { get; set; }
        public int EarnLeave { get; set; }
        public DateTime ValidateUntil { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string CompanyName { get; set; }


    }
}