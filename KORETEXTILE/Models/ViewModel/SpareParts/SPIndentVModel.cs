using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BEEERP.Models.ViewModel.SpareParts
{
    public class SPIndentVModel
    {
        public int IndentNo { get; set; }
        public DateTime IdtDate { get; set; }
        public int CompanyID { get; set; }
        public int TypeID { get; set; }
        public string Description { get; set; }
        public string ReferenceNo { get; set; }
        public int SPItemID { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalValue { get; set; }
        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string UOMName { get; set; }
        [NotMapped]
        public int BalanceQty { get; set; }
    }
}