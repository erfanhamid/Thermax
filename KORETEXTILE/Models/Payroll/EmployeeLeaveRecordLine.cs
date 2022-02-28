using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{   [Table("EmployeeLeaveRecordsLine")]
    public class EmployeeLeaveRecordLine
    {
  
        [Required]
        [Key,Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ELRNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public int LeaveTypeId { get; set; }
        [Required]
        public int LeaveDays { get; set; }
    }
}