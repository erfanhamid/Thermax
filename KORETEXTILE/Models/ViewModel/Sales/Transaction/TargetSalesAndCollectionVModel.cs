using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Sales.Transaction
{
    public class TargetSalesAndCollectionVModel
    {
        [Display(Name = "TSC No")]
        public int TargetSCNo { get; set; }
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }
        [Display(Name = "Period Name")]
        public string PeriodName { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Display(Name = "TSO")]
        public int TsoId { get; set; }
        [Display(Name = "Sales Target")]
        public decimal SalesTarget { get; set; }
        [Display(Name = "Collection Target")]
        public decimal CollectionTarget { get; set; }
        [Display(Name = "Target In Litre")]
        public decimal litreTarget { get; set; }
        [Display(Name = "Total Sales")]
        public decimal TotalSales { get; set; }
        [Display(Name = "Total Litre")]
        public decimal TotalLitre { get; set; }
        [Display(Name = "Total Collection")]
        public decimal TotalCollection { get; set; }
    }
}