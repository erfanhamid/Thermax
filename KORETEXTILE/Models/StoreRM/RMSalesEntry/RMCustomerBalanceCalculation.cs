using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreRM.RMSalesEntry
{
    [Table("tblRMCustomerBalanceCalculation")]
    public class RMCustomerBalanceCalculation
    {
        [Required]
        [Column("clmRMCustomerID")]
        public int RMCustomerID { get; set; }

        [Required]
        [Column("clmDate")]
        public DateTime SalesDate { get; set; }

        [Required]
        [Key, Column("clmAccountID", Order = 2)]
        public int AccountID { get; set; }
        
        [Column("clmRefNo")]
        public string RefNo { get; set; }

        [Column("clmDescription")]
        public string Description { get; set; }

        [Required]
        [Column("clmAmount")]
        public decimal SalesAmount { get; set; }

        [Required]
        [Key, Column("clmDocType", Order = 0)]
        public string DocType { get; set; }

        [Required]
        [Key, Column("clmDocNo", Order = 1)]
        public int DocNo { get; set; }
    }
}