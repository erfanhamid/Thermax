using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.CommercialModule.Settings
{
    [Table("ILCOB")]
    public class ILCOB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ILCOBNO { get; set; }
        [Required]
        public DateTime Date { get; set; }
        
    }
}