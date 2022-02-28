using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.DataAdmin.SPSettings
{
    [Table("SPDepartment")]
    public class SPDepartment
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SPDID { get; set; }
        [Required]
        public string SPDName { get; set; }
    }
}