using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SalesModule
{
    [Table("SalesReturnEntry")]
    public class SalesReturnEntry
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SRNo { get; set; }
        [Required]
        public DateTime SRDate { get; set; }
        [Required]
        public int InvoiceNo { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public int Store { get; set; }
        public string Description { get; set; }
        [Column("TotalRtnAm")]
        public decimal TotalRtnAmount { get; set; }     
        public double TotalQty { get; set; }
        [Column("TotalVatAm")]
        public decimal TotalVatAmount { get; set; }
        [Column("TotalDisAm")]
        public decimal TotalDiscountAmount { get; set; }  
        public int TSO { get; set; }
        public bool IsPrevious { get; set; }
        public decimal TotalCOGS { set; get; }
    }
}