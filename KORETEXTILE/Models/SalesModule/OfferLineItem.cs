using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table("OfferLineItem")]
    public class OfferLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }
        [Required]
        public int OfferNo { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal SalesPercent { get; set; }
        [Required]
        public string SalesCategory { get; set; }
        public int ItemID { get; set; }
        public decimal Unit { get; set; }
        public decimal FreeUnit { get; set; }
        public int CustomerID { get; set; }
        public decimal DiscountOnTotalSale { get; set; }
    }
}