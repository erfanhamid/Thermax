using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class ImportPISummaryNewReport
    {
        public int IPIID { get; set; }
        public string IPINO { get; set; }
        public string SupplierName { get; set; }
        public DateTime PIDate { get; set; }
        public string CompName { get; set; }
        public string CurrencyName { get; set; }
        public string MOP { get; set; }
        public string LoadingPort { get; set; }
        public string DischargePort { get; set; }
        public string Incoterms { get; set; }
        public string BeneficiaryBank { get; set; }
        public string Issuebank { get; set; }
        public decimal Itemtotal { get; set; }
        public decimal FrieghtAmount { get; set; }
        public decimal GrandTotal { get; set; }
    }
}