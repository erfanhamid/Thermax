using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.StoreRM.RMSalesEntry
{
    [Table("tblRMSales")]
    public class TblRMSales
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clmRMSalesNo { get; set; }
        [Required]
        public int clmStoreID { get; set; }
        [Required]
        public int clmCustomerID { get; set; }
        [Required]
        public DateTime clmDate { get; set; }
        public string clmRefNo { get; set; }
        public string clmDescription { get; set; }
        [Required]
        public decimal clmRMSTotal { get; set; }
        [Required]
        public decimal clmCOGSTotal { get; set; }
    }
}