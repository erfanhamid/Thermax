using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement.WorkOrder1     
{
    [Table("tblWorkOrder")]
    public class WorkOrder1
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
        [Column("clmDeliveryDate")]
        public DateTime DeliveryDate { get; set; }
        [Required]
        [Column("clmPaymentTerms")]
        public Int16 PaymentTerms { get; set; }
        [Required]
        [Column("clmRefNo")]
        public string RefNo { get; set; }
        [Required]
        [Column("clmValidDays")]
        public int ValidDays { get; set; }
        [Required]
        [Column("clmWOTotal")]
        public decimal WOTotal { get; set; }
        [Column("clmYPart")]
        public int YPart { get; set; }
        [Column("clmCCode")]
        public int CCode { get; set; }
        [Column("clmSL")]
        public int SLNo { get; set; }
        [Column("clmWOCode")]
        public int WOCode { get; set; }
        [Column("clmDDtOption")]
        public int DDtOption { get; set; }
        [Column("clmPaymentOption")]
        public int PaymentOption { get; set; }
        [Column("clmAddressOption")]
        public int AddressOption { get; set; }
        [Column("clmDdtTerms")]
        public string DdtTerms { get; set; }
        [Column("clmPayTerms")]
        public string PayTerms { get; set; }
        [Column("clmAddressTerms")]
        public string AddressTerms { get; set; }
        public int IDP { get; set; }
        public int YearPart { get; set; }
    }
}