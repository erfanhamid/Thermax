using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.StoreDepot
{
    [Table("tblDSIA")]
    public class DSIA
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmDSIANo")]
        public int DSIANo { get; set; }
        [Required]
        [Column("clmDSIADate")]
        public DateTime DSIADate { get; set; }
        [Required]
        [Column("clmStoreID")]
        public int StoreID { get; set; }
        [Required]
        [Column("clmType")]
        public string Type { get; set; }
        [Column("clmRef")]
        public string Ref { get; set; }
        [Column("clmNote")]
        public string Description { get; set; }
        //[Column("clmDSIANo")]
        //public int? TSoId { get; set; }
        //[Column("clmDSIANo")]
        //public string Category { get; set; }
        [Required]
        [Column("clmTotalQty")]
        public int TotalQty { get; set; }
        [Required]
        [Column("clmTotalValue")]
        public decimal TotalValue { get; set; }
        [Column("clmCOGSTotal")]
        public decimal COGSTotal { get; set; }
        //public decimal TotalVat { get; set; }
    }
}