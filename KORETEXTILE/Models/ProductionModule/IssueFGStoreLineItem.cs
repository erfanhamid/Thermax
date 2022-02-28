using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.ProductionModule
{
    [Table("tblIFGSLineItem")]
    public class IssueFGStoreLineItem
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order =1)]
        //public int Id { get; set; }
        public int clmIFGSNo { get; set; }
        public int clmItemGrpID { get; set; }
        [Required]
        [Key, Column(Order =2)]
        public int clmItemID { get; set; }
        [Required]
        public decimal clmQty { get; set; }
        public decimal clmCostRt { get; set; }
        public decimal clmCostVal { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string PackSize { get; set; }
        [NotMapped]
        public int PcsPerCtn { get; set; }
        [NotMapped]
        public decimal AvailableQty { get; set; }


    }
}