using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("MoneyRequisitionEntry")]
    public class MoneyRequisitionVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SLNO")]
        public int MRVNo { get; set; }
        public int EmpID { get; set; }
        public DateTime Date { get; set; }
        [Column("Accountid")]
        public int AccountId { get; set; } 
        [NotMapped]
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public string RefNo { set; get; }
        public string Description { get; set; }
        [Column("PrevBal")]
        public decimal PrevBalance { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
    }
}