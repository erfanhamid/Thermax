using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ViewModel.SpareParts
{
    public class LMRGoodsReceiveNoteVModel
    {
        [Display(Name = "GRN")]
        public int GRNNo { get; set; }
        [Display(Name = "Date")]
        public DateTime GRNDate { get; set; }
        [Display(Name = "Challan")]
        public string ChallanNo { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        [Display(Name = "WO No")]
        public int? WONo { get; set; }
        [Display(Name = "Store")]
        public int? StoreID { get; set; }
        [Display(Name = "Reference")]
        public string RefNo { get; set; }
        public string Descriptions { get; set; }
        [Display(Name = "Total Qty")]
        public decimal TotalQty { get; set; }
        public decimal? TotalCost { get; set; }
        public string Type { get; set; }
        [Display(Name = "Group")]
        public int? ItemGrpID { get; set; }
        [Display(Name = "Item")]
        public int ItemID { get; set; }
        [Display(Name = "Unit")]
        public int? UOMID { get; set; }
        [Display(Name = "Order Qty")]
        public decimal Qty { get; set; }
        public decimal? CostRt { get; set; }
        public decimal? CostVal { get; set; }
        [Display(Name = "Receive Qty")]
        public int ReceiveQty { get; set; }
        [Display(Name = "Available Qty")]
        public int AvailableQty { get; set; }
        [Display(Name = "Received Qty")]
        public int ReceivedQty { get; set; }
        [Display(Name = "Have To Receive")]
        public int HaveToRecev { get; set; }
        public int WOId { get; set; }
    }
}