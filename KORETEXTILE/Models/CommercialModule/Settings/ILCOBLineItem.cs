using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.CommercialModule.Settings
{
    [Table("ILCOBLineItem")]
    public class ILCOBLineItem
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ILCOBNO { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public string ILCID { get; set; }    
        public decimal ILCOBalance { get; set; }
    }
}