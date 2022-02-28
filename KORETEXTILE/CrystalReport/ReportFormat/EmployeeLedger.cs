using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class EmployeeLedger
    {
        public DateTime Date { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int AccountHead { get; set; }
        public string DocType { get; set; }
        public int DocNum { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}