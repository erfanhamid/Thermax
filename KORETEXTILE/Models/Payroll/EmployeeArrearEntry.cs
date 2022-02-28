using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace BEEERP.Models.Payroll
{
    [Table("EmployeeArrearEntry")]
    public class EmployeeArrearEntry
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [Required]
        public DateTime ARDate { get; set; }
        public string ARRefNO { get; set; }
        public string ARDescription { get; set; }
        [Required]
        public decimal ARAmount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}