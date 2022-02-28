using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.InventoryModule
{
    [Table("InventoryAdditionalInfo")]
    public class InventoryAdditionalInfo
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ShortDescription { get; set; }
        public decimal StandardCost { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal VatPerc { get; set; }
        public int PacSize { get; set; }
        public int CountryID { get; set; }
        public decimal MinStockLevel { get; set; }
        public decimal MaxStockLevel { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal EOQuantity { get; set; }
        [NotMapped]
        public int GroupID { get; set; }
        [NotMapped]
        public string InventoryType { get; set; }
        public string Remarks { get; set; }
    }
}