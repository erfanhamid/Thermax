using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.CommercialVM
{
    public class LTRVModel
    {
        [Display(Name ="LTR ID")]
        public int LTRID { get; set; }
        [Display(Name ="ILC ID")]
        public int LCID { get; set; }
        [Display(Name = "ILC No")]
        public int LCNo { get; set; }
        [Display(Name ="Alt ILC No")]
        public int AltILCNo { get; set; }
        public string Supplier { get; set; }
        [Display(Name ="LTR No")]
        public string LTRNO { get; set; }
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }
        [Display(Name ="Description")]
        public string Description { get; set; }
        [Display(Name ="Select Account")]
        public int ACCID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}