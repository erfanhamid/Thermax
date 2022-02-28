using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ProductionModule
{
    public class FGItemRepackLineItem
    {
        [Required]
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FGIRNo { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int ItemIdOut { get; set; }
        public decimal QtyOut{ get; set; }
        public decimal StanderdCostOut { get; set; }
        [Key, Column(Order = 2)]
        public int ItemIdIn { get; set; }
        public decimal QtyIn { get; set; }
        public decimal StanderdCostIn { get; set; }
        //[Required]
        //public string BatchNo { get; set; }
        [NotMapped]
        public string ItemNameOut { get; set; }
        [NotMapped]
        public string PacSizeOut { get; set; }
        [NotMapped]
        public string ItemNameIn { get; set; }
        [NotMapped]
        public string PacSizeIn { get; set; }
    }
}