using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ProductionModule
{
    public class FGItemRepack
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FGIRNo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string BatchNo { get; set; }
        [Required]
        public string InStore { get; set; }
        [Required]
        public string OutStore { get; set; }
        public string RefNo { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal TotalStanderdCostIn { get; set; }
        [Required]
        public decimal TotalStanderdCostOut { get; set; }   
    }
}