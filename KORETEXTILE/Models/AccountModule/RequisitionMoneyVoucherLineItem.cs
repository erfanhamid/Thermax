using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("RequisitionMoneyVoucherLineItem")]
    public class RequisitionMoneyVoucherLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 0)]
        public int Slno { get; set; }
        [Key, Column(Order = 1)]
        public int DebitAC { get; set; }
        public decimal Amount { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        public int CostGroupID { get; set; }
        [NotMapped]
        public string CostGroupName { get; set; }
        [NotMapped]
        public string DebitACName { get; set; }
    }
}