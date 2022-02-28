using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.AccountModule
{
    [Table("ILCPSubLdgrLine")]
    public class ILCPSubLdgrLine
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column( Order = 0)]
        public int ILCPNo { get; set; }     
        [Required]
        [Key, Column(Order = 1)]
        public int DebitAccID { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int SubLedgerID { get; set; }
        public decimal SlAmount { get; set; } 
        public string SlDescription { get; set; }   
        [NotMapped]
        public string SubLedgerName { get; set; }
    }
}