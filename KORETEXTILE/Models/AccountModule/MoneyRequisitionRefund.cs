using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.AccountModule
{
    [Table("tblRequisitoinedMoneyRefund")]
    public class MoneyRequisitionRefund
    {
        [Key,Column("clmRMRNo")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MRRNo { get; set; }
        [Required]
        [Column("clmEmpID")]
        public int EmpID { get; set; }
        [Required]
        [Column("clmDate")]
        public DateTime Date { get; set; }
        [Required]
        [Column("clmAccountid")]
        public int AccountId { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [Column("clmAmount")]
        public decimal Amount { get; set; }
        [Column("clmRefNo")]
        public string RefNo { set; get; }
        [Column("clmDescription")]
        public string Description { get; set; }
        [NotMapped]
        public int IDP { get; set; }
        [NotMapped]
        public int YearPart { get; set; }
    }
}