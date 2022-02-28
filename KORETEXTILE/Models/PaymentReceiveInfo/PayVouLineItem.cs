using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.PaymentReceiveInfo
{
    [Table("PayVouLineItem")]
    public class PayVouLineItem  
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RPID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int InvoiceNo { get; set; }
        public decimal AdjustedAmount { get; set; }
        [Required]
        public string DocType { set; get; }
    }
}