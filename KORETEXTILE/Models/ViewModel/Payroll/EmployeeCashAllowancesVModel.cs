using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class EmployeeCashAllowancesVModel
    {
       
        public int EmployeeID { get; set; }
        public DateTime CADate { get; set; }
        public decimal CAAmount { get; set; }
        public string CARefNO { get; set; }
        public string CADescription { get; set; }

        public string Name { get; set; }
        public string Designation { get; set; }
    }
}