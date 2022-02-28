using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class EmployeeTransferVModel
    {
        public int TransferID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string PreviousBranch { get; set; }
        public string PresentBranch { get; set; }
        public string TRefNO { get; set; }
        public string TDescription { get; set; }
    }
}