using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class AttandanceLogVModel
    {
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public TimeSpan Late { get; set; }
        public TimeSpan OverTime { get; set; }
        public TimeSpan EarlyLeave { get; set; }
        public int TimeCheck { get; set; }
    }
}