using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.PaymentReceiveInfo
{
    [Table("tblCashAccountConfiguration")]
    public class TblCashAccountConfiguration
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CashBankID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int OperationUnitID { get; set; }
    }
}