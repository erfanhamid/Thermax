using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("EmployeeLeaveRecords")]
    public class EmployeeLeaveRecords
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ELRNo { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public DateTime ELRDate { get; set; }
        [Required]
        public int TotalLeaveDays { get; set; }
        public string RefNo { get; set; }
        public string Descriptions { get; set; }
    }
}