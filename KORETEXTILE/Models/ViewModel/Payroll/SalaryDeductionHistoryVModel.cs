using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class SalaryDeductionHistoryVModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CheckDate { get; set; }
    }
}