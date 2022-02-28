using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.AccountModule
{
    [Table("JVSubLedgerLine")]
    public class JVSubLedgerLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
       
        public int JVNO { get; set; }
        [Required]
        [Column("DeborCre")]
        public string DebOrCre { get; set; }
        //[Key, Column(Order = 2)]
        [Required]
        public int AccountID { get; set; }
        //[Key, Column(Order = 1)]
        [Required]
        public int SubLedgerID { get; set; }
        [Required]
        public decimal JVAmount { get; set; }
        public string Note { get; set; }
        [NotMapped]
        public string SubLedgerName { get; set; }
        public int CostGroupId { get; set; }
        [NotMapped]
        public string CostGroupName { get; set; }
    }
}