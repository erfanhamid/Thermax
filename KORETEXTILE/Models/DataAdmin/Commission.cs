using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Data_Admin
{
    [Table("tblCommission")]
    public class Commission
    {
        [Key]
        [Required]
        public int TDCNo { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime Dateto { get; set; }
        [Required]
        public decimal CommissionPercentage { get; set; }
    }
}