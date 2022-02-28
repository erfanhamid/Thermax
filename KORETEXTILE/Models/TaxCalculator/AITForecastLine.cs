using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.TaxCalculator
{
    [Table("tblAITForcastLine")]
    public class AITForecastLine
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int EmployeeID { get; set; }
        [Required]
        public string WIth_Without { get; set; }
        [Required]
        public decimal MonthlyAIT { get; set; }
        [NotMapped]
        public string EmployeeDesignation { get; set; }
    }
}