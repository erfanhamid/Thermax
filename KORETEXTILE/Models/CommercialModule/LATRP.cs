using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommercialModule
{
    [Table("LATRP")]
    public class LATRP
    {

            [Required]
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int LTRPNo { get; set; }
            [Required]
            public DateTime Date { get; set; }
            [Required]
            public int LTRID { get; set; }
            [Required]
            public int AccountID { get; set; }
            [Required]
            public decimal Amount { get; set; }
            public string Description { get; set; }
            [Required]
            public int ILCId { get; set; }
            public string RefNo { get; set; }
        
    }
}