using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class EmployeeIncrementVModel
    {
        public int IncrementID { get; set; }
        public int EmployeeID { get; set; }
        public string Designation { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int PreviousGross { get; set; }
        public int PresentGross { get; set; }
        public string IRefNO { get; set; }
        public string IDescription { get; set; }
    }
}