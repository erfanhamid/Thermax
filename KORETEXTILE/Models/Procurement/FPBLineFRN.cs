using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Procurement
{
    [Table("tblFPBLineFRN")]
    public class FPBLineFRN
    {
        [Key,Column(Order =0)]
        
        [Required]
        public int BillNo { set; get; }
        [Key, Column(Order = 1)]
        [Required]
        public int FRNNo { set; get; }
    }
}