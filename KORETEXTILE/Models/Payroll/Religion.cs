using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.Payroll
{
    [Table("ReligionInfo")]
    public class Religion
    {
        [Key]
        [Required]
        public int sln { get; set; }
        [Required]
        public string ReligionName { get; set; }
    }
}