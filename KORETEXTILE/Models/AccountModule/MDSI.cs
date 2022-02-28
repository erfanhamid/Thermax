using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.AccountModule
{
    [Table("MDSI")]
    public class MDSI
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int MDSINO { get; set; }
        [Required]
        public DateTime MDSIDate { get; set; }
        [Required]
        public int DepotID { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        [Required]
        public int AccountID { get; set; }
        public decimal TotalAmount { get; set; }
    }

}