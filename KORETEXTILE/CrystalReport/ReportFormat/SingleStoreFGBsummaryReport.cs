using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleStoreFGBsummaryReport
    {
        public string ItemGroup { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemQty { get; set; }
        public decimal AbslQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemAmnt { get; set; }
        public decimal Abslamnt { get; set; }
        public int TRNType { get; set; }
        public DateTime TDate { get; set; }
        public string StoreName { get; set; }
        public string DocType { get; set; }
        public int DocID { get; set; }
        public string UName { get; set; }
        public string RtAccType { get; set; }
        public decimal LiterEquivalent { get; set; }
        public decimal StdCost { get; set; }
        public decimal RetPrice { get; set; }
    }
}