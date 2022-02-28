using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class AttendanceLogResultVModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Late { get; set; }
        public string OverTime { get; set; }
        public string EarlyLeave { get; set; }
    }
}