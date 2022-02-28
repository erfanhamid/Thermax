using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommercialModule
{
    [Table("LCCosting")]
    public class LCCosting
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LCPCNo { get; set; }
        [Required]
        public int ILCID { get; set; }
        [Required]
        public DateTime LCPCDate { get; set; }
        public decimal TotalCostingQty { get; set; }
        public decimal CostingTotal { get; set; }
        [Required]
        public string Type { get; set; }


    }
}