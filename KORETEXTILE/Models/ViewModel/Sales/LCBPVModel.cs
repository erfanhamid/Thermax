using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales
{
    public class LCBPVModel
    {
        [Display(Name ="LCBP No")]
        public int LCBPNo { get; set; }
        [Display(Name = "LC ID")]
        public int LCID { get; set; }
        [Display(Name = "LC No")]
        public string LCNo { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Reference No")]
        public string RefNo { get; set; }
        public string Description { get; set; }
        [Display(Name = "Select A/C")]
        public int AccountID { get; set; }
        public decimal Amount { get; set; }
    }
}