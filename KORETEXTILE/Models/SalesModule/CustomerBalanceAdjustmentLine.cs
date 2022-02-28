using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SalesModule
{
    [Table("CustomerBalanceAdjustmentLine")]
    public class CustomerBalanceAdjustmentLine
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CAANo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvoiceNo { get; set; }
        public decimal Amount { get; set; }
    }
}