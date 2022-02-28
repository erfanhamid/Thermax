using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Procurement
{
    [Table("PurchaseEntry")]
    public class PurchaseEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillNo { set; get; }
        public DateTime PDate { set; get; }
        [Column("SupplierID")]
        public int SupplierId { set; get; }
        public decimal BillTotalValue { set; get; }
        public string PurRef { set; get; }
        public string PurDescription { set; get; }
        [Column("Discountamt")]
        public decimal DiscountAmt { set; get; }
        [Column("NetofDiscount")]
        public decimal TotalDiscount { set; get; }
        [Column("VATAmount")]
        public decimal VatAmount { set; get; }
        [Column("VDAAmnt")]
        public decimal VDAAmnt { set; get; }
        [Column("AITAmount")]
        public decimal AitAmount { set; get; }
        public decimal TotalPayable { set; get; }
        public decimal PaidAmount { set; get; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
    }
}