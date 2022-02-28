using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("tblILCBSubLdgrLine")]
    public class ILCBSubLdgrLine
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 0)]
        public int ILCBNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int DebitAccID { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int SubLedgerID { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string SubLedgerName { get; set; }
       
    }
}