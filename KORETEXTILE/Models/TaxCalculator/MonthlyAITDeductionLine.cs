using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblMonthlyAITDeductionLine")]
    public class MonthlyAITDeductionLine
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int EmployeeID { get; set; }
        public decimal Amount { get; set; }
        public string WIth_Without { get; set; }
        [NotMapped]
        public decimal ForcastAmount { get; set; }
        [NotMapped]
        public string EmployeeDesignation { get; set; }
    }
}