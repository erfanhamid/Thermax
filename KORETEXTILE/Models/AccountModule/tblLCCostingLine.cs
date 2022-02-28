using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("tblLCCostingLine")]
    public class tblLCCostingLine
    {
        [Required]
        [Key, Column("LCPCNo",Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LCPCNo { get; set; }

        [Required]
        [Key, Column("clmItemID", Order = 1)]
        public int ItemID { get; set; }

        [Column("clmLCQty")]
        public decimal LCQty { get; set; }

        [Column("clmPrvQty")]
        public decimal PrvQty { get; set; }

        [Column("clmItemQty")]
        public decimal ItemQty { get; set; }

        [Column("clmItemRate")]
        public decimal ItemRate { get; set; }

        [Column("clmItemValue")]
        public decimal ItemValue { get; set; }

        [NotMapped]
        public string ItemName { get; set; }

        [NotMapped]
        public string Unit { get; set; }

        [NotMapped]
        public string Group { get; set; }
    }
}