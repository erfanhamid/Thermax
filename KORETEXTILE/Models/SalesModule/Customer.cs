using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SalesModule
{
    [Table("Customer")]
    public class Customer
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "Customer ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Credit Limit")]
        public double Credit { get; set; }

        [Required]
        [DisplayName("Opening Balance")]
        public double OB { get; set; }

        [DisplayName("Opening Balance Date")]
        public DateTime AsDate { get; set; }

        [DisplayName("Contact Person")]
        public string ConPerson { get; set; }

        [Required]
        [DisplayName("Mobile No")]
        [RegularExpression("([0-9+]*)", ErrorMessage = "Enter A valid Mobile Number.")]
        public string MobileNo { get; set; }

        [RegularExpression("([0-9+]*)", ErrorMessage = "Enter positive number only")]
        [DisplayName("Telephone No")]
        public string TelephoneNo { get; set; }

        [DisplayName("Email")]
        public string EmailId { get; set; }

        [DisplayName("Billing Address")]
        [Column("Billto")]
        public string BillTo { get; set; }

        [DisplayName("Delivery Address")]
        [Column("Shipto")]
        public string ShipTo { get; set; }

        [Required]
        [DisplayName("Sales Center")]
        [Column("Groupid")]
        public int DepotId { get; set; }

        [Required]
        [DisplayName("Zone")]
        [Column("zoneid")]
        public int ZoneId { get; set; }

        [DisplayName("Area")]
        public string AreaName { get; set; }
        [DisplayName("Customer Type")]
        [NotMapped]
        public string CustomerType { get; set; }
        [NotMapped]
        [Display(Name = "Credit Days Limit")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Enter Integer number only")]
        public int CreditDays { get; set; }

        [NotMapped]
        //[Required]
        [Display(Name = "TSO")]
        public int TSOId { get; set; }

        [NotMapped]
        public string ZoneName { get; set; }
    }
}