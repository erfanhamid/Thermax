using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.StoreRM
{
    public class LCGoodsReceiveNoteVModel
    {
        [Display(Name = "LCRN No")]
        public int LCRNNo { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Challan")]
        public string ChallanNo { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        public int TypeId { get; set; }
        [Display(Name = "LC No")]
        public int? LCNo { get; set; }
        [Display(Name = "Company")]
        public int? CompanyID { get; set; }
        [Display(Name = "Reference")]
        public string RefNo { get; set; }
        public string Description { get; set; }
        [Display(Name = "Total Qty")]
        public decimal TotalQty { get; set; }
        public decimal? TotalCost { get; set; }
        public string Type { get; set; }
        [Display(Name = "Group")]
        public int? GrpId { get; set; }
        [Display(Name = "Item")]
        public int ItemId { get; set; }
        [Display(Name = "Unit")]
        public int? UOMId { get; set; }
        [Display(Name = "Order Qty")]
        public decimal Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Value { get; set; }
        [Display(Name = "Receive Qty")]
        public int ReceiveQty { get; set; }
        [Display(Name = "Available Qty")]
        public int AvailableQty { get; set; }
        [Display(Name = "Received Qty")]
        public int ReceivedQty { get; set; }
        [Display(Name = "Have To Receive")]
        public int HaveToRecev { get; set; }
        public int LCId { get; set; }
        //public int Carton { get; set; }
        //public string LiterKg { get; set; }
        public string UOMName { get; set; }
        public string Lcpcno { get; set; }
    }
}