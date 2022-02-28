using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("StorePurchaseRequisitionLineItem")]
    public class StorePurchaseRequisitionLineItem
    {

        [Required]
        [Key, Column(Order = 0)]
        public int SPRNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int ItemID { get; set; }
        [Required]
        public decimal ItemQty { get; set; }
        [Required]
        public decimal ItemRate { get; set; }
        [Required]
        public decimal ItemValue { get; set; }
        public decimal ItemQtyBalance { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        public string GroupName { get; set; }

    }
}