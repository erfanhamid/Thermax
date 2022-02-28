using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.CommercialVM
{
    public class ILCOBVModel
    {
        [Display(Name = "ILCOB NO")]
        public int ILCOBNO { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "ILC ID")]
        public string ILCID { get; set; }   
        [Display(Name = "ILC OB")]
        public decimal ILCOBalance { get; set; }
        [Display(Name = "As On Date")]
        public DateTime AsonDate { get; set; }
        [Display(Name = "ILC No")]
        public int ILCNo { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
    }
}