using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table("BranchInformation")]
    public class BranchInformation
    {
        [Key]
        [DisplayName("Branch ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("slno")]
        public int Slno { get; set; }
        [DisplayName("Branch Name")]
        [Required]
        public string BrnachName { get; set; }
    }
}