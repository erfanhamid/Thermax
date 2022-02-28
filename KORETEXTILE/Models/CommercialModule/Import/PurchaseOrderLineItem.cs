using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("PurchaseOrderLineItem")]
    public class PurchaseOrderLineItem
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PONo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int ItemID { get; set; }
        [Required]
        public decimal UnitCost { get; set; }
        [Required]
        public int ItemQty { get; set; }
        [Required]
        public decimal TotalCost { get; set; }
        public string Specification { get; set; }
        [Required]
        public int GroupID { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string Unit { get; set; }
        [NotMapped]
        public decimal RevQty { get; set; }
        [NotMapped]
        public decimal AvaQty { get; set; }
        [NotMapped]
        public int UnitId { get; set; }
        [NotMapped]
        public string PackSize { get; set; }
        public double VatPerc { set; get; }
        [NotMapped]
        public int CartonCapcity { get; set; }
        //public double AitPerc { set; get; }
    }
}