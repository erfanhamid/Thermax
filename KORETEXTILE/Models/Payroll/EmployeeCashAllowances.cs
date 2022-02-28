using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace BEEERP.Models.Payroll
{
    [Table("EmployeeCashAllowances")]
    public class EmployeeCashAllowances
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }
        [Required]
        public DateTime CADate { get; set; }
        public string CARefNO { get; set; }
        public string CADescription { get; set; }
        [Required]
        public decimal CAAmount { get; set; }
        [NotMapped]
        public string Name { get; set; }
    }
}