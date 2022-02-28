using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Payroll
{
    [Table("SalaryPaymentLineItem")]
    public class SalaryPaymentLineItem
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SPVNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int EmployeeId { get; set; }
        public decimal SalaryAmount { get; set; }
        [NotMapped]
        public string EmployeeName { get; set; }
    }
}