using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Payroll
{
    public class SalaryPaySlipVModel
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string PaymentAccount { get; set; }
        public string EmployeeCompany { get; set; }
        public int EmployeeId { get; set; }
        public string Designation { get; set; }
        public string ReceivedBy { get; set; }
        public int PVNo { get; set; }
        public string Date { get; set; }
        public int SlNo { get; set; }
        public string DebitAccountHead { get; set; }
        public string Descriptions { get; set; }    
        public decimal Amount { get; set; }
        public string InWords { get; set; }
        public decimal TotalAmount { get; set; }
    }
}