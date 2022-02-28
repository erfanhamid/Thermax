using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Procurement
{
    [Table("SupplierBalanceCalculation")]
    public class SupplierBalanceCalculation
    {
        [Required]
        public int SupplierID { get; set; }
        public DateTime TDate { get; set; }
        [Required]
        [Key, Column(Order = 0)]
        public string DocumentType { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int AccountID { get; set; }
        public string TrDesription { get; set; }
        public string RefNo { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int DocNo { get; set; }
    }
}