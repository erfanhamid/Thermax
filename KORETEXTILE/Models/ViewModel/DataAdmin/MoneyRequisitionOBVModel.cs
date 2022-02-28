using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.DataAdmin
{
    public class MoneyRequisitionOBVModel
    {
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        [Display(Name = "Join Date")]
        public string JoinDate { get; set; }
        public string FD { get; set; }
        [Display(Name = "Work Station")]
        public string WorkStation { get; set; }
        public string Department { get; set; }
        [Display(Name = "EITOB Date")]
        public DateTime MROBDate { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        public string Description { get; set; }
    }
}