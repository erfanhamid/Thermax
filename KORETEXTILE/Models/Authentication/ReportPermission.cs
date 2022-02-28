using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.Authentication
{
    [Table("ReportPermission")]
    public class ReportPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        [Required]
        public string ReportScreenName { get; set; }
        [Required]
        public string ReportName { get; set; }
        public string UserId { get; set; }
    }
}