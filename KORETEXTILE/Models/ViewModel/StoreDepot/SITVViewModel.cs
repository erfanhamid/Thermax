using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.StoreDepot
{
    public class SITVViewModel
    {
        [Display(Name = "Driver Name")]
        public int DriverName { get; set; }
        [Display(Name = "Driver ID")]
        public int DriverID { get; set; }
        [Display(Name = "Vehicle No")]
        public int VehicleID { get; set; }
        [Display(Name = "Shift From")]
        public int StoreID { get; set; }
        [Display(Name = "Depot")]
        public int DepotID { get; set; }
        [Display(Name = "Date")]
        public DateTime SDate { get; set; }
        [Display(Name ="Total Qty")]
        public int TtlQTY { get; set; }
        [Display(Name ="Total Cost")]
        public decimal CostTotal { get; set; }
        [Display(Name = "SITV No")]
        public int SITVNo { get; set; }
        [Display(Name = "Group")]
        public int GroupID { get; set; }
        [Display(Name = "Item")]
        public int ItemID { get; set; }
        [Display(Name = "Shift PCS Qty")]
        public int ShiftQty { get; set; }
        [Display(Name = "Shift CTN Qty")]
        public int ShiftQtyCtn { get; set; }
        [Display(Name ="Balance QTY")]
        public decimal BalanceQty { get; set; }
        [Display(Name = "Cost Rate")]
        public decimal CostRt { get; set; }
        [Display(Name = "Cost Value")]
        public decimal CostVal { get; set; }
        [Display(Name ="PCs Per CTN")]
        public int PcPerCtn { get; set; }
        [Display(Name = "Available CTN")]
        public decimal AvailableCtn { get; set; }
        [Display(Name ="Available PCS")]
        public decimal AvailablePcs { get; set; }
    }
}