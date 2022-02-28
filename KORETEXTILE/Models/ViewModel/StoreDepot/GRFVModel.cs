using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.StoreDepot
{
    public class GRFVModel
    {
        [Display(Name ="FGRN No")]
        public int GRFNo { get; set; }
        [Display(Name ="Depot")]
        public int DepotID { get; set; }
        [Display(Name ="Received Store")]
        public int ReceiveStoreID { get; set; }
        [Display(Name ="Challan No")]
        public string ChallanNo { get; set; }
        [Display(Name ="SIFD No")]
        public int SIFDNo { get; set; }
        [Display(Name ="Vehicle No")]
        public string VehicleNo { get; set; }
        [Display(Name ="Shift From")]
        public string OldStoreID { get; set; }
        [Display(Name ="Description")]
        public string Descriptions { get; set; }
        [Display(Name ="Receive Date")]
        public DateTime ReceiveDate { get; set; }
        [Display(Name ="Challan Date")]
        public DateTime ChallanDate { get; set; }
        [Display(Name ="GRQ No")]
        public int GRQNo { get; set; }
        public int GroupID { get; set; }
        public int ItemID { get; set; }
        [Display(Name ="Group")]
        public string ItemGroup { get; set; }
        [Display(Name ="Item")]
        public string ItemName { get; set; }
        [Display(Name ="PCs PerCTN")]
        public int PcsPerCtn { get; set; }
        [Display(Name ="Issued QTY(CTN)")]
        public int IssuedQtyCtn { get; set; }
        [Display(Name ="Issued QTY(PCs)")]
        public int IssuedQtyPcs { get; set; }
        [Display(Name ="Received QTY(CTN)")]
        public int ReceivedQtyCtn { get; set; }
        [Display(Name ="Received QTY(PCs)")]
        public int ReceivedQtyPcs { get; set; }
        [Display(Name ="Available QTY(CTN)")]
        public int AvailableQtyCtn { get; set; }
        [Display(Name ="Available QTY(PCs)")]
        public int AvailableQtyPcs { get; set; }
        [Display(Name ="Receive QTY(CTN)")]
        public int ReceiveQtyCtn { get; set; }
        [Display(Name ="Receive QTY(PCs)")]
        public int ReceiveQtyPcs { get; set; }
        [Display(Name ="Total Issue Qty")]
        public int TotalIssueQty { get; set; }
        [Display(Name ="Total Receive Qty")]
        public int TotalReceiveQty { get; set; }
    }
}