using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreRM
{
    [Table("tblRMSales")]
    public class RMSales
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("clmRMSalesNo")]
        public int clmRMSalesNo { get; set; }

        [Required]
        [Column("clmStoreID")]
        public int clmStoreID { get; set; }

        [Required]
        [Column("clmCustomerID")]
        public int clmCustomerID { get; set; }

        [Required]
        [Column("clmDate")]
        public DateTime clmDate { get; set; }

        [Column("clmRefNo")]
        public string clmRefNo { get; set; }

        [Column("clmDescription")]
        public string clmDescription { get; set; }

        [Required]
        [Column("clmRMSTotal")]
        public decimal clmRMSTotal { get; set; }

        [Required]
        [Column("clmCOGSTotal")]
        public decimal clmCOGSTotal { get; set; }

    }
}