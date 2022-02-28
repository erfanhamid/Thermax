using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("LMRGoodsReceiveNote")]
    public class LMRGoodsReceiveNote
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GRNNo { get; set; }
        [Required]
        public DateTime GRNDate { get; set; }
        [Required]
        public string ChallanNo { get; set; }
        [Required]
        public int SupplierID { get; set; }
        public int WONo { get; set; }
        public int CompanyID { get; set; }
        public int clmStoreID { get; set; }
        public string RefNo { get; set; }
        public string Descriptions { get; set; }
        [Required]
        public string Type { get; set; }
        [NotMapped]
        public int GroupID { get; set; }
    }
}