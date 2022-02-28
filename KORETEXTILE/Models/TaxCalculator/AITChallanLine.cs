using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblAITChallanLine")]
    public class AITChallanLine
    {
        [Required]
        [Key, Column(Order = 0)]
        public int ID { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int EmployeeID { get; set; }
        [Required]
        public float PercentageOnBalance { get; set; }  
        public decimal Amount { get; set; }
        public string WIth_Without { get; set; }
    }
}