using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommercialModule
{

    [Table("LCCostingLine")]
    public class LCCostingLine
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LCPCNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int ItemID { get; set; }
        public decimal LCQty { get; set; } 
        public decimal PrvQty { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal ItemValue { get; set; }
    }
}