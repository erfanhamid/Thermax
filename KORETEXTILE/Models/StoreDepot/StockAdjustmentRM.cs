using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreDepot
{
    [Table("StockAdjustmentRM")]
    public class StockAdjustmentRM
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int SANo { get; set; }
        [Required]
        public int Store { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string TransectionType { get; set; }
        public string Description { get; set; }
        public string RefNo { get; set; }
        [NotMapped]
        public decimal TotalAmount { get; set; }
    }
}