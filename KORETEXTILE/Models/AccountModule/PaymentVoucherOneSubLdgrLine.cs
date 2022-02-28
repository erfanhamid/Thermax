using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("PaymentVoucherOneSubLdgrLine")]
    public class PaymentVoucherOneSubLdgrLine
    {
        [Required]
        [Key, Column(Order = 0)]
        public int PVNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int DebitAccID { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int SubLedgerID { get; set; }
        public decimal PVAmount { get; set; }
        public string Note { get; set; }
        [NotMapped]
        public string SubLedgerName { get; set; }
    }
}