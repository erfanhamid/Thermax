using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.Payroll
{
    [Table("OpeningLeaveBalance")]
    public class OpeningLeaveBalance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }
        [Required]
        public int CasualLeave { get; set; }
        [Required]
        public int SickLeave { get; set; }
        [Required]
        public int EarnLeave { get; set; }
        public DateTime ValidateUntil { get; set; }
    }
}