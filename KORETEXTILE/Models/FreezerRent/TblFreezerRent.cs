using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.FreezerRent
{
    [Table("tblRent")]
    public class TblFreezerRent
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clmFRRNo { get; set; }
        public int clmDealerID { get; set; }
        public DateTime clmRPDate { get; set; }
        public int clmAccountID { get; set; }
        public int clmDepotID { get; set; }
        public decimal clmBankCharge { get; set; }
        public decimal FrrTotal { get; set; }
    }
}