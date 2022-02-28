using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.Payroll
{
    [Table("MaritalStatus")]
    public class MarriageStatus
    {
        [Key]
        [Required]
        public Int16 slno { get; set; }
        [Required]
        public string mstatus { get; set; }
    }
}