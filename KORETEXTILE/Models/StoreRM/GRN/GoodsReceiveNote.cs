using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.StoreRM.GRN
{
    [Table("tblGoodsReceiveNote")]
    public class GoodsReceiveNote
    {
        [Key,Column("clmGRNNo")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GRNNo { get; set; }
        [Required]
        [Column("clmGRNDate")]
        public DateTime GRNDate { get; set; }
        [Required]
        [Column("clmChallanNo")]
        public string ChallanNo { get; set; }
        [Required]
        [Column("clmSupplierID")]
        public int SupplierID { get; set; }
        public int TypeId { get; set; }
        [Column("clmWONo")]
        public int WONo { get; set;}
        public int CompanyID { get; set; }
        public int clmStoreID { get; set; }
        [Column("clmRefNo")]
        public string RefNo { get; set; }
        [Column("clmDescriptions")]
        public string Descriptions { get; set; }
        [Required]
        [Column("clmType")]
        public string Type { get; set; }
        [NotMapped]
        public int GroupID { get; set; }
    }
}