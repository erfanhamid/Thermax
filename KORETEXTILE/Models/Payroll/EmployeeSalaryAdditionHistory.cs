using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("EmployeeSalaryAdditionHistory")]
    public class EmployeeSalaryAdditionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal Basic { get; set; }
        public decimal HRA { get; set; }
        public decimal MA { get; set; }
        public decimal CA { get; set; }
        public decimal Gross { get; set; }
        public decimal OtherAdj { get; set; }
        public DateTime Date { get; set; }
    }
}