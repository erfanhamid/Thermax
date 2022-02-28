using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("MoneyRequisitionBalanceCalculation")]
    public class MoneyRequisitionBalanceCalculation
    {
        [Required]
        [Key,Column("EmployeeID", Order =0)]
        public int EmpID { set; get; }
        [Column("TDate")]
        public DateTime Date { set; get; }
        [Required]
        [Key, Column(Order = 1)]
        public string DocumentType { set; get; }
        [Required]
        [Key,Column("AccountID", Order = 2)]
        public int AccountId { set; get; }
        [Column("TRDescription")]
        public string Description { set; get; }
        public string RefNo { set; get; }
        public decimal Amount { set; get; }
        [Required]
        [Key, Column("DocNO",Order = 3)]
        public int DocID { set; get; }
    }
}