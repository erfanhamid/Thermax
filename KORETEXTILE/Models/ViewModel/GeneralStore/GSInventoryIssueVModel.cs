using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.ViewModel.GeneralStore
{
    public class GSInventoryIssueVModel
    {

        public int GSIIssueNo { get; set; }
        public DateTime GSIssueDate { get; set; }
        public int StoreID { get; set; }
        public int CompanyID { get; set; }
        public int DepartmentID { get; set; }
        public string ReferenceNo { get; set; }
        public string GSIssueDescription { get; set; }
        public int ItemID { get; set; }
        public int GroupID { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }
        public string UOMName { get; set; }
        public decimal BalanceQty { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalValue { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
    }
}