using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.ViewModel.SpareParts
{
    public class SparePartsDamageVModel
    {
        public int SPDNo { get; set; }
        public DateTime SPDDate { get; set; }
        public int StoreId { get; set; }
        public int BoxID { get; set; }
        public int CompanyID { get; set; }
        public int MachineID { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public int SPItemID { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalValue { get; set; }
        public string UOMName { get; set; }
        [NotMapped]
        public int BalanceQty { get; set; }
        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string ItemTypeName { get; set; }
        [NotMapped]
        public string StoreName { get; set; }
        [NotMapped]
        public string BoxName { get; set; }
    }
}