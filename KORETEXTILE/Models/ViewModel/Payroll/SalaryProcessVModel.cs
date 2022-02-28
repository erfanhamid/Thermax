using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class SalaryProcessVModel
    {
        [Required]
        public int CompanyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [Required]
        public DateTime ProcessDate { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        public int SalaryInfoNo { get; set; }
    }
}