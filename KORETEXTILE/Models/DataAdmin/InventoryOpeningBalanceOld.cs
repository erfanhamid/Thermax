using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.Data_Admin
{
    public class InventoryOpeningBalanceOld
    {

        [Key, Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemID { get; set; }
        [Required]
        public decimal ItemQty { get; set; }
        [Required]
        public decimal ItemRate { get; set; }
        [Required]
        public decimal ItemValue { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int StoreID { get; set; }
        [Required]
        public DateTime IOBDate { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int CompanyID { get; set; }
        [NotMapped]
        public int GroupID { get; set; }
        [NotMapped]
        public string InventoryType { get; set; }
    }
}