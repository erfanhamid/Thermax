using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("EitBalanceCalculation")]
    public class EitBalanceCalculation
    {
        [Required]
        [Key, Column("EmployeeID", Order = 0)]
        public int EmpID { set; get; }
        [Column("TDate")]
        public DateTime Date { set; get; }
        [Required]
        [Key, Column(Order = 1)]
        public string DocType { set; get; }
        [Required]
        [Column("AccountID")]
        public int AccountId { set; get; }
        [Column("TDescriptions")]
        public string Description { set; get; }
        public string TReference { set; get; }
        public decimal TAmount { set; get; }
        [Required]
        [Key, Column("DocNo", Order = 2)]
        public int DocID { set; get; }
    }
}