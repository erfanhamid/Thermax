using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class MRVAPreviewReport
    {
        public string CostCenter { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int MRVANo { get; set; }
        public DateTime MRVADate { get; set; }
        public int Code { get; set; }
        public string AccountHead { get; set; }
        public string CostGroup { get; set; }
        public string Description { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
    }
}