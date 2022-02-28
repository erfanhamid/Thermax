using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BEEERP.Models.ViewModel.StoreDepot
{
    public class FGRNViewModel
    {
        [Display(Name = "FGRN No")]
        public int GRFNo { get; set; }
        [Display(Name = "Depot")]
        public int DepotID { get; set; }
        [Display(Name = "Received Store")]
        public int IssueTo { get; set; }
        [Display(Name = "Challan No")]
        public string ChallanNo { get; set; }
        [Display(Name = "SIFD No")]
        public int SIFDNo { get; set; }
        [Display(Name = "Shift From")]
        public int IssueFrom { get; set; }
        public string Descriptions { get; set; }
        [Display(Name = "Received Date")]
        public DateTime ReceiveDate { get; set; }
        [Display(Name = "Challan Date")]
        public DateTime ChallanDate { get; set; }
        public int? VehicleID { get; set; }
        
        [Display(Name = "Total Received Qty")]
        public int TotalQty { get; set; }
        [Display(Name = "Total Issue Qty")]
        public int TotalIssueQty { get; set; }  
        public decimal TotalCost { get; set; }

        [Display(Name = "Group")]
        public int GroupID { get; set; }
        [Display(Name = "Item")]
        public int ItemID { get; set; }
        
        public decimal Rate { get; set; }
        public decimal Cost { get; set; }

        [Display(Name = "Issue Qty(CTN)")]
        public int IssueQtyCtn { get; set; }
        [Display(Name = "Issue Qty(PCs)")]
        public int IssueQty { get; set; }

        [Display(Name = "Received Qty(CTN)")]
        public int RcvdQtyCtn { get; set; }
        [Display(Name = "Received Qty(PCs)")]
        public int RcvdQty { get; set; }    

        [Display(Name = "Available Qty(CTN)")]
        public int AvailableQtyCtn { get; set; }
        [Display(Name = "Available Qty(PCs)")]
        public int AvailableQty { get; set; }

        [Display(Name = "Receive Qty(CTN)")]
        public int RcvQtyCtn { get; set; }
        [Display(Name = "Receive Qty")]
        public int Qty { get; set; }
        [Display(Name = "PCS Per CTN")]
        public int PCsPerCtn { get; set; }
    }
}