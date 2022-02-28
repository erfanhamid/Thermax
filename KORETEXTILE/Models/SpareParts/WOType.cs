using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.SpareParts
{
    [Table("WOType")]
    public class WOType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WOTID { get; set; }
        [Required]
        public string WOTypeName { get; set; }
    }
}