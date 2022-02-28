using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.FreezerRent
{
    [Table("tblRentLineItem")]
    public class TblFreezerRentLineItem
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clmFRRNo { get; set; }
        [Key]
        [Column(Order = 1)]
        public int clmRetailerID { get; set; }
        public string clmRef { get; set; }
        public string clmDescription { get; set; }
        public decimal clmAmount { get; set; }
        public string clmDealerYN { get; set; }
        [NotMapped]
        public string RetailerName { get; set; }
    }
}