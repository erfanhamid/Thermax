using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.CommercialVM
{
    public class DueILCBBill
    {
        public int ILCBNo { set; get; }
        public decimal DueAmount { set; get; }
        public int SubLedgerId { set; get; }
    }
}