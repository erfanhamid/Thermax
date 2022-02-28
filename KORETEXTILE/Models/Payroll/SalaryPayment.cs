using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Payroll
{
    [Table("SalaryPayment")]
    public class SalaryPayment
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SPVNo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public String Paymode { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
