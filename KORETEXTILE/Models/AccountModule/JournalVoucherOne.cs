using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("JournalVoucherOne")]
    public class JournalVoucherOne
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JVNO { get; set; }
        [Required]
        public DateTime JVDate { get; set; }
        public string RefNo { get; set; }
        public decimal TotalAmount { get; set; }
        [Column("Branchid")]
        public int BranchId { get; set; }
        //[Column("CostGroupid")]
       // public int CostGroupId { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
    }
}