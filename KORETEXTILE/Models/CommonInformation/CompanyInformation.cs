using BEEERP.Models.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BEEERP.Models.CommonInformation
{
    [Table("CompanyInformation") ]
    public  class CompanyInformation
    {
      
        [Key]
        public int Id { set; get; }
        [Column("CompName")]
        public  string CompanyName { set; get; }
        [Column("CompAddress")]
        public  string CompanyAddress { set; get; }
        [Column("ContactNo")]
        public  string ConNo { set; get; }
        [Column("VatRegistrationNo")]
        public  string VatREgNo { set; get; }
        [Column("BINNo")]
        public string BINNo { set; get; }
        [Column("TINNo")]
        public  string TINNo { set; get; }
        [Column("FactoryAddress")]
        public  string FactoryAddress { set; get; }
        [Column("FactoryContact")]
        public  string FactoryContact { set; get; }
        [Column("FaxNo")]
        public string Fax { set; get; }
        public string TelephoneNo { set; get; }
        public string Email { set; get; }
        public DateTime StartDate { set; get; }
        public string FirstMonthofFinYear { set; get; }
        public readonly  BEEContext context = new BEEContext();
        public void InitializeCompanyInformation()
        {
            var company = context.CompanyInformation.FirstOrDefault();
            CompanyAddress = company.CompanyAddress;
            CompanyName = company.CompanyName;
            ConNo = company.ConNo;
            VatREgNo = company.VatREgNo;
            BINNo = company.BINNo;
            TINNo = company.TINNo;
            FactoryAddress = company.FactoryAddress ;
            FactoryContact = company.FactoryContact;
            TelephoneNo = company.TelephoneNo;
            Email = company.Email;
            StartDate = company.StartDate;
            FirstMonthofFinYear = company.FirstMonthofFinYear;

        }
    }
}