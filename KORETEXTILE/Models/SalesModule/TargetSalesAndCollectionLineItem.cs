using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SalesModule
{
    [Table("TargetSalesAndCollectionLineItem")]
    public class TargetSalesAndCollectionLineItem
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TargetSCNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int TsoId { get; set; }
        public decimal SalesTarget { get; set; }
        public decimal litreTarget { get; set; }
        public decimal CollectionTarget { get; set; }
        [NotMapped]
        public string TsoName { get; set; }
    }
}