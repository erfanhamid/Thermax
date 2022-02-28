using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.ProductionModule
{
    [Table("tblRMCEntry")]
    public class RMCEntry
    {
        [Key]
        [Column("clmRMCNo")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RMCNo { get; set; }
        [Column("clmRMCDate")]
        public DateTime RMCDate { get; set; }
        [Column("clmBatchID")]
        public int BatchID { get; set; }
        [Column("clmStoreID")]
        public int StoreId { get; set; }
        [StringLength(50)]
        [Column("clmRefNo")]
        public string RefNo { get; set; }
        [Column("clmDescriptions")]
        public string Descriptions { get; set; }
        [Column("clmRMCTotalQty")]
        public decimal RMCTotalQty { set; get; }
        public decimal RMCTotalValue { get; set; }
        [NotMapped]
        public string JobNo { get; set; }
        [NotMapped]
        public string DocType { get; set; }
    }
}