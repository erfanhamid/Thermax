using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ViewModel.Sales.Report
{
    public class ReportVModel
    {
        public DateTime From { set; get; }
        public DateTime To { set; get; }
        public DateTime AsOnDate { set; get; }
        [Display(Name ="Ledger A/C")]
        public int LedgerAC { set; get; }
        public int DepotId { set; get; }
        [Display(Name ="Customer ID")]
        public int CustomerID { set; get; }
        [Display(Name = "Customer Name")]
        public string CustName { set; get; }
        public int Depot { set; get; }
        public int Group { get; set; }
        public int Item { get; set; }
        public int SalesManId { get; set; }
        public string SalesManName { get; set; }
        public DateTime FromR1 { get; set; }
        public DateTime ToR1 { get; set; }
        public DateTime FromR2 { get; set; }
        public DateTime ToR2 { get; set; }
        public int TSOId { set; get; }
        public int RSMId { set; get; }
        public int StoreId { set; get; }
        [DisplayName("ILC ID")]
        public int ILCID { get; set; }
        [DisplayName("ILC NO")]
        public string ILCNO { get; set; }
        public int SupplierID { get; set; }
        public int SupplierGroup { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        public int EmployeeID { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [DisplayName("LTR ID")]
        public int LTRID { get; set; }
        [DisplayName("LTR NO")]
        public string LTRNO { get; set; }
        public int DebitAccID { get; set; }
        public int SubLedgerID { get; set; }
        public string CategoryType { get; set; }
        public int LedgerGroup { get; set; }
        public int GroupID { get; set; }
        public int ItemID { get; set; }
        public int RetailerId { get; set; }
        public string VehicleType { get; set; }
        public int CostGroup { get; set; }
        [DisplayName("Cost Center")]
        public int CostCenterID { get; set; }

    }
}