using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEERP.Models.HRModule
{
    [Table("Employee")]
    public class Employee
    {
        [Required]
        [Key][Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        public string FatherName { set; get; }
        public string MotherName { set; get; }
        public string PresentAddress { set; get; }
        public string PermanentAddress { set; get; }
        public string Mobile { set; get; }
        public string Email { set; get; }
        [Required]
        public DateTime DateOfBirth { set; get; }
        public DateTime JoiningDate { set; get; }
        public DateTime? ConfirmationDate { get; set; }
        [Required]
        public int Designation { set; get; }
        public int HighestEducation { set; get; }
        public int DepatmentID { set; get; }
        public string AlternativeContct { set; get; }
        public string PersonalEmail { set; get; }
        public int Section { set; get; }
        public int BranchID { set; get; }
        public string LogInUserName { set; get; }
        public int CompanyId { set; get; }
        [Required]
        [Display(Name="Status")]
        public int Status { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string DesignationName { get; set; }
        [NotMapped]
        public  string FD { get; set; }
        [NotMapped]
        public string WorkStation { get; set; }
        [NotMapped]
        public string Department { get; set; }
        [NotMapped]
        public string SectionName { get; set; }
        [NotMapped]
        public string HigherEdu { get; set; }
        [NotMapped]
        public string ReligionName { get; set; }
        [NotMapped]
        public string GenderName { get; set; }
        [NotMapped]
        public string MaritalStatus { get; set; }
    }
}