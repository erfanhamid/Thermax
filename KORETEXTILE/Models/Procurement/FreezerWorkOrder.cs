using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement
{
    [Table("tblFWO")]
    public class FreezerWorkOrder
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
        public int PaymentTerms { get; set; }
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
        public int SL { get; set; }
        [Column("clmWOCode")]
        public int WOCode { get; set; }
        [Column("clmDDtOption")]
        public int DdtOption { get; set; }
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
        [NotMapped]
        public decimal TotalVat { set; get; }
        [NotMapped]
        public decimal TotalAit { set; get; }
        [NotMapped]
        public int IDP { get; set; }
        [NotMapped]
        public int YearPart { get; set; }
        [NotMapped]
        public int SupplierGroup { get; set; }
        public decimal VatAmount { get; set; }
        public decimal AitAmount { get; set; }











        
       
    }
}