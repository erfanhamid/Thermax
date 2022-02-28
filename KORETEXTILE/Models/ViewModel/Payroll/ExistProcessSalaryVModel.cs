using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class ExistProcessSalaryVModel
    {
        public int SalaryInfoNo { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int CompanyId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}