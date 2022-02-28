using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("PaymentVoucherOne")]
    public class PaymentVoucherOne
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PVNo { get; set; }
        [Required]
        public DateTime PVDate { get; set; }
        [Required]
        public int CashBankID { get; set; }
        [Required]
        [Column("PayeeName")]
        public string ReceiverName { get; set; }
        public string RefNo { get; set; }
        [Required]
        public int SalesCenterID { get; set; }
        [Required]
        public decimal PVTotalAmount { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
    }
}