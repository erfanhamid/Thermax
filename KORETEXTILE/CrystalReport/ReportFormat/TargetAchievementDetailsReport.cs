using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.CrystalReport.ReportFormat
{
    public class TargetAchievementDetailsReport
    {
        public string Area { get; set; }
        public string Incharge { get; set; }
        public string TsoName { get; set; }
        public decimal SalesTargetAmount { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public decimal SalesTargetLitre { get; set; }
        public decimal TotalSalesLitre { get; set; }
        public decimal Achievements { get; set; }
        public decimal Deviation { get; set; }
        public decimal Outstanding { get; set; }
        public decimal CollectionBudget { get; set; }
        public decimal ActualCollection { get; set; }
        public decimal ActualCollectionP { get; set; }
        public decimal DeviationP { get; set; }

    }
}