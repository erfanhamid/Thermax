using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.PaymentReceiveInfo
{
    [Table("AdvanceAmountLineItem")]
    public class AdvanceAmountLineItem
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AANo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int InvoiceNo { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int RPID { get; set; }   
        public decimal AdjustedAmount { get; set; } 

    }
}