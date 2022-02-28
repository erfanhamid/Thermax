using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table("tblRetailer")]
    public class RetailerInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }

        [Required]
        public string RetailerName { get; set; }
        public int CustomerID { get; set; }
        public string ConPerson { get; set; }
        public string MobileNo { get; set; }
        public string TelephoneNo { get; set; }
        public string EmailId { get; set; }
        public string Billto { get; set; }
        public string Shipto { get; set; }
        public string DealerYesNo { get; set; }
        public decimal OB { get; set; }
        [NotMapped]
        public string DocType { get; set; }
    }
}