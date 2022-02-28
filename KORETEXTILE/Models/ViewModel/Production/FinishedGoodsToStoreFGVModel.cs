using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Production
{
    public class FinishedGoodsToStoreFGVModel
    {
        public int FGPNo { set; get; }
        [DisplayName("Group Name")]
        public int GroupID { get; set; }
        [DisplayName("Item Name")]
        public int ItemID { set; get; }
        public decimal Qty { set; get; }
        public decimal CogsRate { set; get; }
        public decimal CogsTotal { set; get; }
        public DateTime FGPDate { set; get; }
        public int BatchID { set; get; }
        public int StoreID { set; get; }
        [DisplayName("Reference No")]
        public string RefNo { set; get; }
        public string Descriptions { set; get; }
        public decimal TotalQty { set; get; }
        [DisplayName("UoM")]
        public string UMO { set; get; }
        public double PerUnitRate { set; get; }
        public double TotalCost { set; get; }
        [DisplayName("Target Qty")]
        public double TQty { get; set; }
        [DisplayName("Received Qty")]
        public double RcvQty { get; set; }
        public decimal PCPerCtn { get; set; }
        public decimal ProductionQtyCtn { get; set; }
    }
}