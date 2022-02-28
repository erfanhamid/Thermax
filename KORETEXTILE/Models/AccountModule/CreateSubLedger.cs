using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.AccountModule
{
    [Table("CreateSubLedger")]
    public class CreateSubLedger
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubLedgerID { get; set; }
        [Required]
        public string SubLedgerName { get; set; }
        [Required]
        public string SubLedgerType { get; set; }
        [Required]
        public int DepotID { get; set; }
        public string ReferenceNo { get; set; }
        public string SLDescription { get; set; }
        [NotMapped]
        public string DepotName { get; set; }
    }
}