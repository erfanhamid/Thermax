using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("tblDealerAccAdjustment")]
    public class DealerAccAdjustment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmDAANo")]
        public int DAANo { get; set; }
        [Required]
        [Column("clmDAADate")]
        public DateTime DAADate { get; set; }
        [Required]
        [Column("clmDepotID")]
        public int DepotID { get; set; }
        public int CostGroup { get; set; }
        [Required]
        [Column("clmDAAAccountID")]
        public int DAAAccountID { get; set; }
        [Column("clmDescription")]
        public string Description { get; set; }
        [Column("clmTotalAmount")]
        public decimal TotalAmount { get; set; }
        [NotMapped]
        public int IDP { get; set; }
        [NotMapped]
        public int YearPart { get; set; }
    }
}