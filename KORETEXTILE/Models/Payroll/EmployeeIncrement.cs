using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("EmployeeIncrement")]
    public class EmployeeIncrement
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IncrementID { get; set; }
        public int EmployeeID { get; set; }
        [Required]
        public DateTime EffectiveDate { get; set; }
        public decimal PreviousGross { get; set; }
        public decimal PresentGross { get; set; }

        public string IRefNO { get; set; }
        public string IDescription { get; set; }
        
    }
}