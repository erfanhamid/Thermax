using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.InventoryModule
{
    [Table("InventoryOpeningBalance")]
    public class InventoryOpeningBalance
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupId { get; set; }
        [Key, Column(Order = 0)]
        [Required]
        public int ItemId { get; set; }
        [Required]
        public decimal ItemQty { get; set; }
        [Required]
        public decimal ItemRate { get; set; }
        [Required]
        public decimal ItemValue { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int StoreId { get; set; }
        public int BoxID { get; set; }
        [Required]
        public DateTime IOBDate { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int CompanyID { get; set; }
        public string PageNo { get; set; }
        [NotMapped]
        public string InventoryType { get; set; }

    }
}