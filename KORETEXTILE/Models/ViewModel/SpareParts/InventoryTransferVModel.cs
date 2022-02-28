using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.SpareParts
{
    public class InventoryTransferVModel
    {
        public int ITNo { get; set; }
        public DateTime ITDate { get; set; }
        public int OldCompanyID { get; set; }
        public int TypeID { get; set; }
        public int NewCompanyID { get; set; }
        public string RefNo { get; set; }
        public string ITDescription { get; set; }
        public int ItemID { get; set; }
        public int OldStoreID { get; set; }
        public int OldBoxID { get; set; }
        public int NewStoreID { get; set; }
        public int NewBoxID { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }
    }
}