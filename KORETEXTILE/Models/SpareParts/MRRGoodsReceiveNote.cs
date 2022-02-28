using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("MRRGoodsReceiveNote")]
    public class MRRGoodsReceiveNote
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LCRNNo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int QCNo { get; set; }
        public int CompanyID { get; set; }
        [Required]
        public int TypeId { get; set; }
        public string RefNo { get; set; }
        public string Descriptions { get; set; }
        public string Type { get; set; }
        [NotMapped]
        public int GroupID { get; set; }
    }
}