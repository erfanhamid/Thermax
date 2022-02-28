using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("JobQuittingInformation")]
    public class JobQuittingInformation
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JQINo { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int ROLId { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public string EmployeeName { get; set; }
        [NotMapped]
        public string Designation { get; set; }
    }
}