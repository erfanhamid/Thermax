using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Report
{
    public class InventoryReportVModel
    {
        public int DepotID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { set; get; }
        public string Group { set; get; }
        [DataType(DataType.Date)]
        public DateTime FromDate { set; get; }
        [DataType(DataType.Date)]
        public DateTime ToDate { set; get; }
        public string Item { set; get; }
        [DataType(DataType.Date)]
        public DateTime AsOnDate { set; get; }
        public int FGStoreId { set; get; }
        public int StoreFor { get; set; }
        public string RootAccountType { get; set; }
        [Display(Name ="Item Name")]
        public int ItemID { get; set; }
        [Display(Name ="Group Name")]
        public int GroupID { get; set; }
        public int SupplierGroup { get; set; }
        public int FGGroupID { get; set; }
        public int FGItemID { get; set; }
        public int BatchID { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public int DepartmentID { get; set; }
        public int ItemTypeID { get; set; }
        public int SPItemID { get; set; }
        public int StoreId { set; get; }
        public int CompanyID { set; get; }
        public int MachineID { set; get; }
        public int SPTypeID { set; get; }
        public int SPBoxID { set; get; }
    }
}