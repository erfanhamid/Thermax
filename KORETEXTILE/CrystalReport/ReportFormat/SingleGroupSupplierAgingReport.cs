using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class SingleGroupSupplierAgingReport
    {
        public string Grp { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int SuppCode { get; set; }
        public DateTime AsOnDt { get; set; }
        public decimal Balance { get; set; }
        public decimal ADV { get; set; }
        public decimal ThirtyDays { get; set; }
        public decimal SixtyDays { get; set; }
        public decimal NinetyDays { get; set; }
        public decimal AbvNinetyDays { get; set; }
    }
}