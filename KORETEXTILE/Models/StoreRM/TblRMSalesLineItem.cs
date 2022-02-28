using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreRM
{
    [Table("tblRMSalesLineItem")]
    public class TblRMSalesLineItem
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column("clmRMSalesNo", Order = 0)]
        public int clmRMSalesNo { get; set; }
        [Key, Column("clmItemID", Order = 1)]
        public int clmItemID { get; set; }
        public decimal clmItemQty { get; set; }
        public decimal clmItemRate { get; set; }
        public decimal clmItemValue { get; set; }
        public decimal clmCOGSRate { get; set; }
        public decimal clmCOGSTotal { get; set; }
        [NotMapped]
        public string itemName { get; set; }
        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
    }
}