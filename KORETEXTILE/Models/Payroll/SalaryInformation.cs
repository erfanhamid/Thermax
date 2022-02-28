using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("SalaryInformation")]
    public class SalaryInformation
    {
        [Key][Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SalaryInfoNo { get; set; }
        
        public int Month { get; set; }
        
        public int Year { get; set; }
        [Required]
        public DateTime ProcessDate { get; set; }
        [Required]
        public double TotalPayableSalary { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
    }
}