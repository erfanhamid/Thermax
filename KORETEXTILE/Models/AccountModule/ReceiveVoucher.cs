using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.AccountModule
{
    [Table("ReceiveVoucher")]
    public class ReceiveVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RVNo { set; get; }
        public DateTime RVDate { set; get; }
        public decimal RVAmount { set; get; }
        [Required]
        public int ReceiveAccountID { set; get; }
        [Required]
        public int CreditAccountID { set; get; }
        public string PayeeName { set; get; }
        public string Description { set; get; }
        public string RefNo { get; set; }
        public int BranchID { set; get; }
        public int CostGroupID { set; get; }
        public int IDP { get; set; }
        public int YearPart { get; set; }

    }
}