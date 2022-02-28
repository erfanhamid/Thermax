using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement
{
    [Table("PayBillILine")]
    public class PayBillLine
    {
        [Key]
        [Column(Order = 0)]
        public int PBID { get; set; }
        [Key]
        [Column("BilllNo", Order = 1)]
        public int BillNo { get; set; }
        [Key]
        [Column(Order = 2)]
        public string BillType { get; set; }
        [Column("BillAmount")]
        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal Remaining { get; set; }
        public int SPAANo { get; set; }
    }
}