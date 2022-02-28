using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommercialModule
{   
    [Table("LCCostingQtyBalance")]
    public class LCCostingQtyBalance
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ILCID { get; set; }
        [Required]
        public int ItemID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public string DocType { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int DocNo { get; set; }
        public decimal Qty { get; set; }
    }
}