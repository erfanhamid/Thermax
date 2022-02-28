using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("QualityControlLineItem")]
    public class QualityControlLineItem
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int QcNo { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int ItemID { get; set; }
        [Required]
        public decimal Qty { get; set; }
        [Required]
        public decimal QcQty { get; set; }
        [Required]
        public decimal CostRt { get; set; }
        [Required]
        public decimal CostVal { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        public string GroupName { get; set; }
        [NotMapped]
        public string UoM { get; set; }
    }
}