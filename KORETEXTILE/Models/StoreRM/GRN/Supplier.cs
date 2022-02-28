using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEEERP.Models.StoreRM.GRN
{
    [Table("Supplier")]
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public int GroupID { get; set; }
        public double Credit { get; set; }
        public double OB { get; set; }
        public DateTime AsDate { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }
        public string TelephoneNo { get; set; }
        public string EmailId { get; set; }
        [Required]
        public string Address { get; set; }
        public string SCode { get; set; }
        [NotMapped]
        public string BankAccountNumber { get; set; }
        [NotMapped]
        public string BankName { get; set; }
        [NotMapped]
        public string BankAddress { get; set; }
    }
}