using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement
{
    [Table("tblWorkOrder")]
    public class WorkOrder
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmWONo")]
        public int WONo { get; set; }
        [Required]
        [Column("clmWODate")]
        public DateTime WODate { get; set; }
        [Required]
        [Column("clmSupplierID")]
        public int SupplierID { get; set; }
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public int WOTypeID { get; set; }
        [Required]
        [Column("clmDeliveryDate")]
        public DateTime DeliveryDate { get; set; }
        [Required]
        [Column("clmPaymentTerms")]
        public int PaymentTerms { get; set; }
        [Required]
        [Column("clmValidDays")]
        public int ValidDays { get; set; }
        [Column("clmWOTotal")]
        public decimal WOTotal { get; set; }
        //[Column("clmWOCode")]
        //public int WOCode { get; set; }      
        public decimal VatAmount { get; set; }
        public decimal VdsAmount { get; set; }
        public decimal AitAmount { get; set; }

    }
}