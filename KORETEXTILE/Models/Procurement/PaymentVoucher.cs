using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement
{
    [Table("PaymentVoucher")]
    public class PaymentVoucher
    {
        [Required]
        [Key]
        public int PVNo { get; set; }
        public DateTime PVDate { get; set; }
        [Required]
        public decimal PVAmount { get; set; }
        [Required]
        public int CashBankAccID { get; set; }
        [Required]
        public int DebitAccountID { get; set; }
        public string PayeeName { get; set; }
        public string Description { get; set; }
        public string RefNo { get; set; }
        public int SalesCenterID { get; set; }
    }
}