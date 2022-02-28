using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.AccountModule
{
    [Table("LCPVSubLedgerLine")]
    public class LCPVSubLedgerLine
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 0)]
        public int LCPVNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int GLAccountID { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int SubLedgerID { get; set; }
        [Required]
        public decimal TAmount { get; set; }
        public string TDescription { get; set; }
        [NotMapped]
        public string SubLedgerName { get; set; }
    }
}