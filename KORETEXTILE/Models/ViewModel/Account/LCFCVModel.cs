using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Account
{
    public class LCFCVModel
    {
        public int LCFCNo { get; set; }
        public int ILCNo { get; set; }
        public DateTime LCFCDate { get; set; }
        public string IALCNo { get; set; }
        public string ILCType { get; set; }
        public int SupplierID { get; set; }

        public decimal TotalQty { get; set; }
        public decimal ILCTotal { get; set; }
        public decimal TotalAllow { get; set; }
        public decimal ToBeAllow { get; set; }

        public string ItemGroup { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal Unit { get; set; }
        public decimal LCQty { get; set; }
        public decimal PrevQty { get; set; }
        public decimal FinalQty { get; set; }
        public decimal Value { get; set; }
        public decimal Rate { get; set; }

        public decimal TQtyOfCost { get; set; }
        public decimal ILCTCost { get; set; }
        public decimal TCAllow { get; set; }
        public decimal CToBeAllow { get; set; }
    }
}