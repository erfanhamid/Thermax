using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Procurement
{
    [Table("PurchaseLineGRN")]
    public class PurchaseLineGRN
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }
        [Required]
        public int BillNo { set; get; }
        [Required]
        public int GRNNo { set; get; }
    }
}