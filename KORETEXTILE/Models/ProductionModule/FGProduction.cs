using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.ProductionModule
{
    [Table("tblProduction")]
    public class FGProduction
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmFGPNo")]
        public int FGPNo { set; get; }
        [Column("clmFGPDate")]
        public DateTime FGPDate { set; get; }
        [Required]
        [Column("clmBatchID")]
        public int BatchID { set; get; }
        [Column("clmStoreID")]
        public int StoreID { set; get; }
        [Column("clmRefNo")]
        public string RefNo { set; get; }
        [Column("clmDescriptions")]
        public string Descriptions { set; get; }
        [Column("clmTotalQty")]
        public decimal TotalQty { set; get; }
        [NotMapped]
        public string ItemName { set; get; }
    }
}