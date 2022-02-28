using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.CommercialModule.Import
{
    [Table("tblILCBLine")]
    public class ILCBLine
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ILCBNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int DebitAccID { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string DebitAccName { get; set; }
    }
}