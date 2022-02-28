using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.StoreFG
{
    public class IssueRMToFGVModel
    {
        [Display(Name = "IRTF No")]
        public int IRTFNo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Issue From")]
        public int IssueFrom { get; set; }
        [Display(Name = "Issue To")]
        public int IssueTo { get; set; }
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }
        public string Description { get; set; }
        [Display(Name = "Total RM Qty")]
        public double TotalRMQty { get; set; }
        [Display(Name = "Total RM Standerd Cost")]
        public double TotalRMStanderdCost { get; set; }
        [Display(Name = "Total FG Qty")]
        public double TotalFGQty { get; set; }
        [Display(Name = "Total FG Standerd Cost")]
        public double TotalFGStanderdCost { get; set; }
        
        [Display(Name = "RM Item")]
        public int RMItemId { get; set; }
        [Display(Name = "RM Qty")]
        public double RMQty { get; set; }
        [Display(Name = "RM Standerd Cost")]
        public double RMStanderdCost { get; set; }
        public double RMTotal { get; set; }
        [Display(Name = "FG Item")]
        public int FGItemId { get; set; }
        [Display(Name = "FG Qty")]
        public double FGQty { get; set; }
        [Display(Name = "FG Standerd Cost")]
        public double FGStanderdCost { get; set; }
        public double FGTotal { get; set; }
        [Display(Name = "Available Qty")]
        public double RemQty { get; set; }
    }
}