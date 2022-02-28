using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.GeneralStore
{
    [Table("GSWorkOrder")]
    public class GSWorkOrder
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WONo { get; set; }
        [Required]
        public DateTime WODate { get; set; }
        [Required]
        public int SupplierID { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public int PaymentTerms { get; set; }
        [Required]
        public string RefNo { get; set; }
        [Required]
        public int ValidDays { get; set; }
        [Required]
        public decimal WOTotal { get; set; }
        public int CCode { get; set; }
        public int SL { get; set; }
        public int WOCode { get; set; }
        public int DdtOption { get; set; }
        public int PaymentOption { get; set; }
        public int AddressOption { get; set; }
        public string DdtTerms { get; set; }
        public string PayTerms { get; set; }
        public string AddressTerms { get; set; }
        [NotMapped]
        public decimal TotalVat { set; get; }
        [NotMapped]
        public decimal TotalAit { set; get; }
        [NotMapped]
        public int SupplierGroup { get; set; }
        [NotMapped]
        public decimal VatAmount { get; set; }
        public decimal VdsAmount { get; set; }
        public decimal AitAmount { get; set; }
    }
}