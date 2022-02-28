using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.CommonInformation
{
    [Table("CompanySetup")]
    public class CompanySetup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int ID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public string Year { get; set; }
        public bool HolidayRules { get; set; }
    }
}