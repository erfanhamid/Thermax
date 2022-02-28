using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.DataAdmin.SPSettings
{
    [Table("SpOpeningBalance")]
    public class SpOpeningBalance
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int GroupId { get; set; }
        [Required]
        [Key, Column(Order = 0)]
        public int ItemId { get; set; }
        [Required]
        public DateTime SpDate { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal Rate { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int TypeId { get; set; }
        [Required]
        public int StoreId { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int CompanyID { get; set; }
        [Required]
        [Key, Column(Order = 3)]
        public int BoxID { get; set; }
        public string PageNo { get; set; }

    }
}