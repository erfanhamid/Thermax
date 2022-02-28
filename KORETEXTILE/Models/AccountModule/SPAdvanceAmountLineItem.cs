using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("SPAdvanceAmountLineItem")]
    public class SPAdvanceAmountLineItem
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SPAANo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int BillNo { get; set; }
        [Required]
        public string BillType { get; set; }
        [Required]
        public decimal AdjustedAmount { get; set; }
        [NotMapped]
        public decimal Amount { get; set; }
        [NotMapped]
        public decimal Balance { get; set; }
        [NotMapped]
        public decimal billamount { get; set; }
        [NotMapped]
        public decimal Remaining { get; set; }
    }
}