using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEEERP.Models.Payroll
{
    public class EmployeeSalaryAddition
    {
        [Key]
        //        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }
        public decimal Basic { get; set; }
        public decimal HRA { get; set; }
        public decimal MA { get; set; }
        public decimal CA { get; set; }
        public decimal Gross { get; set; }
        public decimal OtherAdj { get; set; }
    }
}