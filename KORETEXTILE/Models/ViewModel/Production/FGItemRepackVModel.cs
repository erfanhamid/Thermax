using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.Production
{
    public class FGItemRepackVModel
    {
        [Display(Name = "FGIRNo")]
        public int FGIRNo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Batch No")]
        public string BatchNo { get; set; }
        [Display(Name = "In Store")]
        public string InStore { get; set; }
        [Display(Name = "Out Store")]
        public string OutStore { get; set; }
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }
        public string Description { get; set; }
        [Display(Name = "Total Standerd Cost In")]
        public decimal TotalStanderdCostIn { get; set; }
        [Display(Name = "Total Standerd Cost Out")]
        public decimal TotalStanderdCostOut { get; set; }
        [Display(Name = "Item")]
        public int ItemIdOut { get; set; }
        [Display(Name = "Qty")]
        public decimal QtyOut { get; set; }
        [Display(Name = "s Cost Out")]
        public decimal StanderdCostOut { get; set; }
        [Display(Name = "Item")]
        public int ItemIdIn { get; set; }
        [Display(Name = "Qty")]
        public decimal QtyIn { get; set; }
        [Display(Name = "S Cost In")]
        public decimal StanderdCostIn { get; set; }
        [Display(Name = "Remaining Qty")]
        public decimal RemQty { get; set; }
        [Display(Name = "Remaining Qty Out")]
        public decimal RemQtyOut { get; set; }
    }
}