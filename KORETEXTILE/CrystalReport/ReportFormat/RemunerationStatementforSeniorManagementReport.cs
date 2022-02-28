using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class RemunerationStatementforSeniorManagementReport
    {
        public string CompanyName { get; set; }
        public string NameOfEmployee { get; set; }
        public int IDNo { get; set; }
        public string Designaion { get; set; }
        public DateTime JoningDate { get; set; }
        public double Basic { get; set; }
        public double HRent { get; set; }
        public double Conv { get; set; }
        public double Medical { get; set; }
        public double GrossSalary { get; set; }
        public int NoOfDay { get; set; }
        public int Pay { get; set; }
        public double EarnedSalary { get; set; }
        public decimal OvertimeHourDay { get; set; }
        public double OvertimeTaka { get; set; }
        public double Arrear { get; set; }
        public double TotalAddition { get; set; }
        public double AdvanceAdj { get; set; }
        public double Tax { get; set; }
        public double TMPhone { get; set; }
        public double PFOwn { get; set; }
        public double OtherAdj { get; set; }
        public double TotalDeduct { get; set; }
        public double PayableSalary { get; set; }
        public string Bank { get; set; }
        public string Cash { get; set; }
        public string Signature { get; set; }
        public double Loan { get; set; }
        public double TDS { get; set; }
        public decimal TA { get; set; }
        public decimal OtherAddition { get; set; }
        public DateTime ProcessDate { get; set; }

    }
}