using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("IssueSpareParts")]
    public class IssueSpareParts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int ISPNo { get; set; }
        [Required]
        public DateTime ISPDate { get; set; }
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public int MachineID { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int SRNo { get; set; }
        [Required]
        public DateTime SRDate { get; set; }
        public string Description { get; set; }

    }
}