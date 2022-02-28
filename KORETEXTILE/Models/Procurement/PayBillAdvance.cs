using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Procurement
{
    [Table("PayBillAdv")]
    public class PayBillAdvance
    {
        [Key, Column(Order =0)]
        public int PBID { get; set; }
        [Key, Column(Order = 2)]
        public int AdvBillNo { get; set; }
        [Key, Column(Order = 1)]
        public string BillType { get; set; }
        public decimal BillAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal Remaining { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
    }
}