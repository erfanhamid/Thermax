using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.SalesModule
{
    [Table("SalesReturnLineItem")]
    public class SalesReturnLineItem
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int SRNo { get; set; }
        [Required]
        public int ItemID { get; set; } 
        [Required]
        public double ReturnQty { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Value { get; set; }
        public decimal Price { get; set; }
        public decimal RtnOfferQty { get; set; }
        public decimal ClmCostPrice { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public double SalesQty { get; set; }
        [NotMapped]
        public double OfferQty { get; set; }
    }
}