using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.DataAdmin.SPSettings
{
    [Table("SPAdditionalInfo")]
    public class SPAdditionalInfo
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemID { get; set; }
        [Required]
        public decimal StandardCost { get; set; }
        public decimal MinStockLevel { get; set; }
        public decimal MaxStockLevel { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal EconomicOQ { get; set; }
        [Required]
        public int CountryID { get; set; }
        public string Remarks { get; set; }
        [NotMapped]
        public int DepartmentID { get; set; }
        [NotMapped]
        public int SPTypeID { get; set; }
    }
}