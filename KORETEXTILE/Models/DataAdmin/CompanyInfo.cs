using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.Data_Admin
{
    [Table("CompanyInfo")]
    public class CompanyInfo
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { set; get; }
        [Required]
        public string CompanyName { set; get; }
        public string CompAddress { set; get; }
        public string ContactNo { set; get; }
        public string VatRegistrationNo { set; get; }
        public string BINNo { set; get; }
        public string TINNo { set; get; }
        public string FactoryAddress { set; get; }
        public string FactoryContact { set; get; }
        public string FaxNo { set; get; }
        public string TelephoneNo { set; get; }
        public string Email { set; get; }
        public DateTime StartDate { set; get; }
        public string FirstMonthofFinYear { set; get; }
    }
}