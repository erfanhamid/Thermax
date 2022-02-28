using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Payroll
{
    [Table("MAR")]
    public class MonthlyAttendance
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MARID { get; set; }
        public DateTime MARDate { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }

    }
}