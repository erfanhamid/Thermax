using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class JobQuittingVModel
    {
        [Display(Name = "JQI No")]
        public int JQINo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        [Display(Name = "Reason Of Leaving")]
        public int ROLId { get; set; }
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        public string Description { get; set; }
    }
}