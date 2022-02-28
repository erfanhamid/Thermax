using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.DataAdmin
{
    public class ScreenEntryLockVModel
    {
        [Display(Name ="Module")]
        public int ModuleID { get; set; }
        [Display(Name="Screen")]
        public int ScreenID { get; set; }
        [Display(Name ="Date")]
        public DateTime SELDate { get; set; }
    }
}