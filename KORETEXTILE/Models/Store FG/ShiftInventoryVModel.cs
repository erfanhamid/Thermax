using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Store_FG
{
    public class ShiftInventoryVModel
    {
        [Display(Name = "SIFD No")]                                 
        public int SIFDNo { get; set; }
        public int Depot { get; set; }
        [Display(Name = "Shift From")]
        public int CurrentStoreID { get; set; }
        [Display(Name = "Shift TO")]
        public int NewStoreID { get; set; }
        public int VehicleID { get; set; }
        public int DriverID { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "GRQ No")]
        public int GRQNo { get; set; }
        [Display(Name = "Challan No")]
        public int ChallanNo { get; set; }  
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Total")]
        public int TtlQTY { get; set; }
        public decimal TotalCost { get; set; }
        [Display(Name = "Group")]
        public int ItemGrpID { get; set; }
        [Display(Name = "Item")]
        public int ItemID { get; set; }
        public int ShiftQty { get; set; }
        public decimal CostRt { get; set; }
        public decimal CostVal { get; set; }    
        [Display(Name = "PCs Per CTN")]
        public int PCsPerCTN { get; set; }
        [Display(Name = "Available CTN")]
        public int AvailableCTN { get; set; }
        public int AvailablePCs { get; set; }
        [Display(Name = "Shift CTN Qty")]
        public int ShiftCTNQty { get; set; }
        [Display(Name = "Shift PCs Qty")]
        public int ShiftPCsQty { get; set; }
        [Display(Name = "Balance Qty")]
        public int BalanceQty { get; set; }
    }
}