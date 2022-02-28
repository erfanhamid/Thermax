using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class EmployeeArrearEntryVModel
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public DateTime ARDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal ARAmount { get; set; }
        public string ARRefNO { get; set; }
        public string ARDescription { get; set; }

        
        public string Designation { get; set; }

    }
}