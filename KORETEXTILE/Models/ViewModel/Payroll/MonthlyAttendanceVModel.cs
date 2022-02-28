using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class MonthlyAttendanceVModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int MAR_ID { get; set; }
        public int Date { get; set; }
        public string employee { get; set; }
        public int Absent { get; set; }
        public int PDays { get; set; }
        public int WDays { get; set; }
        public int Depot { get; set; }
    }
}