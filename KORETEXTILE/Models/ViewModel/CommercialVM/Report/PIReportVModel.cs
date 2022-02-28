using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.CommercialVM.Report
{
    public class PIReportVModel
    {
        public DateTime From { set; get; }
        public DateTime To { set; get; }
        public string Type { set; get; }
        public int SupplierId { get; set; }
        public int Item { get; set; }
    }
}