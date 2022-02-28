using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("MRRGoodsReceiveNoteLineItem")]
    public class MRRGoodsReceiveNoteLineItem
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int LCRNNo { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int ItemID { get; set; }
        public int StoreId { get; set; }
        public int BoxID { get; set; }
        [Required]
        public decimal QcQty { get; set; }
        public decimal CostRt { get; set; }
        public decimal CostVal { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string StoreName { get; set; }
        [NotMapped]
        public string BoxName { get; set; }
    }
}