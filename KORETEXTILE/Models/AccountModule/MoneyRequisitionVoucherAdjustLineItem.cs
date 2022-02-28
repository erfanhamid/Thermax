using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.AccountModule
{
    [Table("MoneyRequisitionVoucherAdjustLineItem")]
    public class MoneyRequisitionVoucherAdjustLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 0)]
        public int MRVANo { get; set; }
        [Key, Column(Order = 1)]
        public int DebitAcId { get; set; }
        public int CostGroupId { get; set; }
        public decimal MRVAAmount { get; set; }
        public string MRVADescription { get; set; }
        public string RefNo { get; set; }
        [NotMapped]
        public string CostGroupName { get; set; }
        [NotMapped]
        public string DebitACName { get; set; }

    }
}