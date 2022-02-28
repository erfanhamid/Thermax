using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblAITChallan")]
    public class AITChallan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string FinancialYear { get; set; }
        [Required]
        public int Corporation { get; set; }
        [Required]
        public string Location { get; set; }
        public decimal TotalAmount { get; set; }
        [Required]
        public string ChallanNo { get; set; }
        public string Description { get; set; }
    }
}