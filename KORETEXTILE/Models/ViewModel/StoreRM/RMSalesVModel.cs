using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.StoreRM
{
    public class RMSalesVModel
    {
        public int clmRMSalesNo { get; set; }
        public int clmStoreID { get; set; }
        public int clmCustomerID { get; set; }
        public DateTime clmDate { get; set; }
        public string clmRefNo { get; set; }
        public string clmDescription { get; set; }
        public decimal clmRMSTotal { get; set; }
        public decimal clmCOGSTotal { get; set; }

        public int clmItemID { get; set; }
        public decimal clmItemQty { get; set; }
        public decimal clmItemRate { get; set; }
        public decimal clmItemValue { get; set; }
        public decimal clmCOGSRate { get; set; }

        public int balanceQty { get; set; }
        public int GroupId { get; set; }
        public decimal RMSTotal { get; set; }
        public decimal COGSTotal { get; set; }
        
    }
}