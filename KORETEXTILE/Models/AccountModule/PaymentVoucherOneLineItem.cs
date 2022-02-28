using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.AccountModule
{
    [Table("PaymentVoucherOneLineItem")]
    public class PaymentVoucherOneLineItem
    {
        [Required]
        [Key,Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PVNo { get; set; }
        [Required]
        public int CostGroupID { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int DebitAccID { get; set; }
        [Required]
        public decimal RVAmount { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string DebitAccName { get; set; }
        [NotMapped]
        public string CostGroupName { get; set; }   
    }
}