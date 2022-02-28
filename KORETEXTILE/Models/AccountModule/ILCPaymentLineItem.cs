using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("ILCPaymentLineItem")]
    public class ILCPaymentLineItem
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 0)]
        public int ILCPNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int DebitAccID { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string DebitAccName { get; set; }
    }
}