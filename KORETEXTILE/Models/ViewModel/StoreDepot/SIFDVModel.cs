using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.StoreDepot
{
    public class SIFDVModel
    {
        [Display(Name ="SIID No")]
        public int SIFDNo { get; set; }
        public int Depot { get; set; }
        [Display(Name ="Shift From")]
        public int CurrentStoreID { get; set; }
        [Display(Name ="Shift To")]
        public int NewStoreID { get; set; }
        [Display(Name ="Vehicle No")]
        public int VehicleID { get; set; }
        [Display(Name ="Driver ID")]
        public int DriverID { get; set; }
        [Display(Name = "Driver Name")]
        public int DriverName { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "GRQ No")]
        public int GRQNo { get; set; }
        [Display(Name = "Challan No")]
        public int ChallanNo { get; set; }
        public string Description { get; set; }
        [Display(Name ="Total Qty")]
        public int TtlQTY { get; set; }
        [Display(Name ="Total Cost")]
        public decimal TotalCost { get; set; }
        [Display(Name ="Group")]
        public int ItemGrpID { get; set; }
        [Display(Name ="Item")]
        public int ItemID { get; set; }
        [Display(Name = "Shift PCS Qty")]
        public int ShiftQty { get; set; }
        public decimal CostRt { get; set; }
        public decimal CostVal { get; set; }
        [Display(Name = "PCs Per CTN")]
        public int PcPerCtn { get; set; }
        [Display(Name = "Available CTN")]
        public decimal AvailableCtn { get; set; }
        [Display(Name = "Available PCS")]
        public decimal AvailablePcs { get; set; }
        [Display(Name = "Shift CTN Qty")]
        public int ShiftQtyCtn { get; set; }
        [Display(Name = "Balance QTY")]
        public decimal BalanceQty { get; set; }
    }
}