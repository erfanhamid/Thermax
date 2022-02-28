using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("InventoryTransferLineItem")]
    public class InventoryTransferLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int ITNo { get; set; }
        [Required]
        public int ItemID { get; set; }
        [Required]
        public int OldStoreID { get; set; }
        [Required]
        public int OldBoxID { get; set; }
        [Required]
        public int NewStoreID { get; set; }
        [Required]
        public int NewBoxID { get; set; }
        [Required]
        public decimal ItemQty { get; set; }
        [Required]
        public decimal ItemRate { get; set; }
        [Required]
        public decimal ItemValue { get; set; }
    }
}