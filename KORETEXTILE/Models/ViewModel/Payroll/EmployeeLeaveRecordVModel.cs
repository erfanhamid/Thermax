using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class EmployeeLeaveRecordVModel
    {
        [DisplayName("ELR No")]
        public int ELRNo { get; set; }
        [DisplayName("Employee ID")]
        public int EmployeeID { get; set; }
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Designation")]
        public string Designation { get; set; }
        [DisplayName("Join Date")]
        public DateTime JoinDate { get; set; }
        [DisplayName("FD")]
        public string FD { get; set; }
        [DisplayName("Work Station")]
        public string WorkStation { get; set; }
        [DisplayName("Department")]
        public string Department { get; set; }
        [DisplayName("Leave Type")]
        public  int LeaveTypeID { get; set; }
        [Display(Name ="Leave Balance")]
        public int LeaveBalance { get; set; }
        [DisplayName("Al Days")]
        public int AlDays { get; set; }
        [DisplayName("From")]
        public DateTime FromDate { get; set; }
        [DisplayName("To")]
        public DateTime ToDate { get; set; }
        [DisplayName("Leave Days")]
        public int LeaveDays {get; set; }
        [DisplayName("Ref No")]
        public string RefNo { get; set; }
        [DisplayName("Descriptions")]
        public string Description { get; set; }
        [DisplayName("Total Leave Days")]
        public int TotalLeaveDays { get; set; }
    }
}