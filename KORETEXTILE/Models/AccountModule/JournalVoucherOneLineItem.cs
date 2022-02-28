using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("JournalVoucherOneLineItem")]
    public class JournalVoucherOneLineItem
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int JVNO { get; set; }
        public decimal JVAmount { get; set; }
        [Required]
        [Column("DeborCre")]
        public string DebOrCre { get; set; }
        public string Description { get; set; }
        [Required]
        //[Key, Column(Order = 1)]
        public int AccountHeadID { get; set; }
        [NotMapped]
        public string AccountHeadName { get; set; }
        public int CostGroupId { get; set; }
        [NotMapped]
        public string CostGroupName { get; set; }
    }
}