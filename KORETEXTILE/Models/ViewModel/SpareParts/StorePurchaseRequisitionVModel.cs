using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.SpareParts
{
    public class StorePurchaseRequisitionVModel
    {
        public int SPRNo { get; set; }
        public DateTime SPRDate { get; set; }
        public string SPDescriptions { get; set; }
        public string SPReference { get; set; }
        public int SPDepartmentID { get; set; }


        public int GroupID { get; set; }
        public int ItemID { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }
        public decimal ItemQtyBalance { get; set; }
        public int StoreID { get; set; }
        public string UoM { get; set; }


    }
}