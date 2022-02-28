using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.SpareParts
{
    [Table("SPIndent")]
    public class SPIndent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int IndentNo { get; set; }
        [Required]
        public DateTime IdtDate { get; set; }
        [Required]
        public int TypeID { get; set; }
        [Required]
        public int CompanyID { get; set; }
        public string Description { get; set; }
        public string ReferenceNo { get; set; }
    }
}