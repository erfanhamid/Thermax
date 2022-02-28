using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblMonthlyAITDeduction")]
    public class MonthlyAITDeduction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string FinancialYear { get; set; }
        [Required]
        public string Month { get; set; }
        [Required]
        public int Corporation { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
    }
}