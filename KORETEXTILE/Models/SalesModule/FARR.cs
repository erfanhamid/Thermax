using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table ("tblRentRefund")]
    public class FARR
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clmFARRNo { get; set; }
        public int clmDealerID { get; set; }
        public DateTime clmDate { get; set; }
        public int clmAccountID { get; set; }
        public int clmDepotID { get; set; }
        public decimal clmTotalAmount { get; set; }
        public string clmType { get; set; }
    }
}