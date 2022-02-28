using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("tblLCCostingQtBalance")]
    public class tblLCCostingQtBalance
    {
        [Required]
        [Key, Column("clmILCID", Order = 0)]
        public int ILCID { get; set; }
        [Required]
        [Key, Column("ItemID", Order = 1)]
        public int ItemID { get; set; }
        [Required]
        [Column("clmDate")]
        public DateTime Date { get; set; }
        [Required]
        [Key, Column("clmDocType", Order = 2)]
        public string DocType { get; set; }
        [Required]
        [Key, Column("clmDocNo", Order = 3)]
        public int DocNo { get; set; }
        [Required]
        [Column("clmQty")]
        public decimal Qty { get; set; }
    }
}