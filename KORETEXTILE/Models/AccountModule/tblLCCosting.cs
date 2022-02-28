using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("tblLCCosting")]
    public class tblLCCosting
    {
        [Required]
        [Key, Column("LCPCNo")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LCPCNo { get; set; }

        [Required]
        [Column("clmILCID")]
        public int ILCID { get; set; }

        [Required]
        [Column("clmLCPCDate")]
        public DateTime LCPCDate { get; set; }

        [Required]
        [Column("clmTotalCostingQty")]
        public int TotalCostingQty { get; set; }

        [Required]
        [Column("clmIICostingTotal")]
        public decimal ILCCostingTotal { get; set; }

        [Required]
        [Column("Type")]
        public string Type { get; set; }

        [NotMapped]
        public int supplierID { get; set; }
    }
}