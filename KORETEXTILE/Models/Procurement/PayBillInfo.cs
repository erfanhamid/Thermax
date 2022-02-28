using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement
{
    [Table("PayBillInfo")]
    public class PayBillInfo
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PBID { get; set; }
        public DateTime TDate { get; set; }
        public string CreditAdvance { get; set; }
        [Column("GroupID")]
        public int GroupId { get; set; }
        [Required]
        [Column("SupplierID")]
        public int SupplierId { get; set; }
        [Required]
        [Column("PaymentAccID")]
        public int PaymentAccId { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public decimal Amount { get; set; }
        [NotMapped]
        public string DocType { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
        public decimal PaidAmt { get; set; }
        public decimal PrevBal { get; set; }
    }
}