using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.ViewModel.Account
{
    public class EPVVModel
    {
        [Display(Name = "EPV No")]
        public int EPVNo { get; set; }
        [Display(Name = "Pay A/C")]
        public int PayAccId { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }
        public string Description { get; set; }
        [Display(Name = "Total")]
        public decimal EPVTotal { get; set; }
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        [Display(Name = "Work Station")]
        public string WorkStation { get; set; }
        [Display(Name = "Organic Designation")]
        public string OrganicDesignation { get; set; }
        [Display(Name = "Functional Designation")]
        public string FunctionalDesignation { get; set; }

    }
}