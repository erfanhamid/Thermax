using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("PurchaseOrder")]
    public class PurchaseOrder
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PONo { get; set; }
        [Required]
        public DateTime PODate { get; set; }
        [Required]
        public int SupplierID { get; set; }
        public string RefNo { get; set; }
        [Required]
        public decimal POTotal { get; set; }
        public decimal TotalVat { set; get; }
        public double TotalQty { get; set; }
        public string ReferenceNo { get; set; }
        public string PoSubject { get; set; }
        public string ContactPerson { get; set; }
        public string Others { get; set; }
        [NotMapped]
        public int SupplierGroup { get; set; }
    }
}